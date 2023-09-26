using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    [SerializeField] GameState _currentState;
    
    public enum GameState
    {
        InGame,
        Paused,
        GameOver,
    }
    public GameState CurrentState { get { return _currentState; } }
     bool _exchangeUI;
    protected override void OnAwake()
    {
        Init();
    }
    void Init()
    {
        GameObject _itemSaver = new GameObject();
        _itemSaver.AddComponent<ItemSaver>();
        GameObject _exchangeSystem = new GameObject();
        _exchangeSystem.AddComponent<ExchangeSystem>();
        GenericSingleton<ItemSaver>.Instance.Init();
        GenericSingleton<ExchangeSystem>.Instance.Init();
        GenericSingleton<UIBase>.Instance.Init();
        GenericSingleton<WeaponManager>.Instance.Init();
        GenericSingleton<PlayerCon>.Instance.Init();
        SetGameState(GameState.InGame);
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!GenericSingleton<UIBase>.Instance.ExchangeOn)
            {
                SetGameState(GameState.Paused);
                GenericSingleton<UIBase>.Instance.AllUIOff();
                GenericSingleton<UIBase>.Instance.ShowExchangeUI(true);
            }
            else
            {
                SetGameState(GameState.InGame);
                GenericSingleton<UIBase>.Instance.ShowExchangeUI(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!GenericSingleton<UIBase>.Instance.WeaponOn)
            {
                SetGameState(GameState.Paused);
                GenericSingleton<UIBase>.Instance.AllUIOff();
                GenericSingleton<UIBase>.Instance.ShowWeaponSelectUI(true);
            }
            else
            {
                SetGameState(GameState.InGame);
                GenericSingleton<UIBase>.Instance.ShowWeaponSelectUI(false);
            }   
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetGameState(GameState.InGame);
            GenericSingleton<UIBase>.Instance.AllUIOff();
        }
        float deltaTime = 0.0f;

        void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }

    }
    public void SetGameState(GameState newState)
    {
        _currentState = newState;
        switch (_currentState)
        {
            case GameState.Paused:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.InGame:
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }
    
}
