using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    GameState _currentState;
    [SerializeField] GameState _startState;
    public enum GameState
    {
        Loading,
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
        GameObject dataManager = new GameObject();
        dataManager.name = "DataManager";
        dataManager.AddComponent<DataManager>();
        GameObject itemSaver = new GameObject();
        itemSaver.name = "ItemSaver";
        itemSaver.AddComponent<ItemSaver>();
        GameObject exchangeSystem = new GameObject();
        exchangeSystem.name = "ExchangeSystem";
        exchangeSystem.AddComponent<ExchangeSystem>();
        GenericSingleton<ItemSaver>.Instance.Init();
        GenericSingleton<ExchangeSystem>.Instance.Init();
        GenericSingleton<WeaponManager>.Instance.Init();
        GenericSingleton<UIBase>.Instance.Init();
        GenericSingleton<PlayerCon>.Instance.Init();
        GenericSingleton<DataManager>.Instance.Init();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SetGameState(_startState);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //    GenericSingleton<DataManager>.Instance.SaveData(0);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    GenericSingleton<DataManager>.Instance.LoadData(0);
        //}
        if (_currentState != GameState.Loading)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!GenericSingleton<UIBase>.Instance.ExchangeOn)
                {
                    if (!GenericSingleton<UIBase>.Instance.PauseUIOn)
                    {

                        GenericSingleton<UIBase>.Instance.InvenWeaponOff();
                        GenericSingleton<UIBase>.Instance.ShowExchangeUI(true);
                    }

                }
                else
                {
                    GenericSingleton<UIBase>.Instance.ShowExchangeUI(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (!GenericSingleton<UIBase>.Instance.WeaponOn)
                {
                    if (!GenericSingleton<UIBase>.Instance.PauseUIOn)
                    {
                        GenericSingleton<UIBase>.Instance.InvenWeaponOff();
                        GenericSingleton<UIBase>.Instance.ShowWeaponSelectUI(true);
                    }

                }
                else
                {
                    GenericSingleton<UIBase>.Instance.ShowWeaponSelectUI(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_currentState == GameState.Paused)
                {
                    if (GenericSingleton<UIBase>.Instance.ExchangeOn || GenericSingleton<UIBase>.Instance.WeaponOn)
                    {
                        GenericSingleton<UIBase>.Instance.InvenWeaponOff();
                    }
                    else
                    {
                        GenericSingleton<UIBase>.Instance.ShowPauseUI(false);
                    }
                }
                else if (_currentState == GameState.InGame)
                {
                    GenericSingleton<UIBase>.Instance.ShowPauseUI(true);
                }


            }
        }
        Debug.Log(_currentState);
        


    }
    public void SetGameState(GameState newState)
    {
        _currentState = newState;
        switch (_currentState)
        {

            case GameState.Loading:
                GenericSingleton<UIBase>.Instance.CrossHairOff();
                GenericSingleton<UIBase>.Instance.ShowPlayerUI(false);
                GenericSingleton<PlayerCon>.Instance.Camera.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                GenericSingleton<UIBase>.Instance.CrossHairOff();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.InGame:
                Time.timeScale = 1;
                GenericSingleton<UIBase>.Instance.ShowPlayerUI(true);
                GenericSingleton<PlayerCon>.Instance.Camera.gameObject.SetActive(true);
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
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       switch (scene.name)
       {
            case "YDDesert":
                SetGameState(GameState.InGame);
                Debug.Log("사막신 로드완료");
                GenericSingleton<UIBase>.Instance.SetCrosshair();
                GenericSingleton<ParentSingleTon>.Instance.SetPosition(new Vector3(0, 0, 0));
                GenericSingleton<ParentSingleTon>.Instance.SetRotation(0);
                GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(0, 0, 0));
                GenericSingleton<PlayerCon>.Instance.SetRotation(0);
                break;
            case "YDNeonCity":
                SetGameState(GameState.InGame);
                Debug.Log("네온시티신 로드완료");
                GenericSingleton<UIBase>.Instance.SetCrosshair();
                GenericSingleton<ParentSingleTon>.Instance.SetPosition(new Vector3(-2.5f, 1.5f, -15));
                GenericSingleton<ParentSingleTon>.Instance.SetRotation(0);
                GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(0, 0, 0));
                GenericSingleton<PlayerCon>.Instance.SetRotation(0);
                break;
            case "YDUnderCity":
                SetGameState(GameState.InGame);
                Debug.Log("언더시티신 로드완료");
                GenericSingleton<UIBase>.Instance.SetCrosshair();
                GenericSingleton<ParentSingleTon>.Instance.SetPosition(new Vector3(15, 1.5f, 46));
                GenericSingleton<ParentSingleTon>.Instance.SetRotation(0);
                GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(0, 0, 0));
                GenericSingleton<PlayerCon>.Instance.SetRotation(0);
                break;
            case "YDBossStage":
                SetGameState(GameState.InGame);
                Debug.Log("보스신 로드완료");
                GenericSingleton<UIBase>.Instance.SetCrosshair();
                GenericSingleton<ParentSingleTon>.Instance.SetPosition(new Vector3(-27, 1.5f, 16));
                GenericSingleton<ParentSingleTon>.Instance.SetRotation(0);
                GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(0, 0, 0));
                GenericSingleton<PlayerCon>.Instance.SetRotation(90);
                break;
        }
    }

}
