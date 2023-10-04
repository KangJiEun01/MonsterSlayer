using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIBase : GenericSingleton<UIBase>
{
    [SerializeField] GameObject _weaponUI;
    [SerializeField] GameObject _weaponSelectUI;
    [SerializeField] GameObject _exchangeUI;
    [SerializeField] GameObject _gamoverUI;
    [SerializeField] GameObject _runToggleUI;
    [SerializeField] GameObject _dashCoolUI;
    [SerializeField] GameObject _HPBarUI;
    [SerializeField] GameObject _pauseUI;
    [SerializeField] GameObject _inventoryUI;
    [SerializeField] GameObject _warningUI;
    [SerializeField] GameObject _crossHairs;
    [SerializeField] GameObject _playerUI;
    [SerializeField] TextMeshProUGUI _fpsUI;
    [SerializeField] GameObject _fadeOut;
    [SerializeField] Sprite[] _itemIcon;
    public Sprite[] ItemIcon { get { return _itemIcon; } }

    bool _weaponOn;
    public bool WeaponOn { get { return _weaponOn; } }
    bool _exchangeOn;
    public bool ExchangeOn { get { return _exchangeOn;} }
    bool _pauseUIOn;
    public bool PauseUIOn { get { return _pauseUIOn; } }


    float _timer = 0;
    int _frame = 0;
    private void Update()
    {
        CalFPS();
        RunToggleUIUpdate();
        DashCoolUIUIUpdate();
    }
    void RunToggleUIUpdate()
    {
        _runToggleUI.GetComponent<RunToggleUI>().RunToggleUIUpdate();
    }
    void DashCoolUIUIUpdate()
    {
        _dashCoolUI.GetComponent<DashCoolUI>().DashCoolUIUpdate();
    }
    void CalFPS()
    {
        _timer += Time.deltaTime;
        _frame++;
        if (_timer > 1)
        {
            _fpsUI.text = $"FPS : {_frame}";
            _timer = 0;
            _frame = 0;
        }
    }
    public void Init()
    {
        _inventoryUI.GetComponent<Inventory>().Init();
        _exchangeUI.GetComponent<ExchangeUI>().Init();
        SetCrosshair(GenericSingleton<WeaponManager>.Instance.CurrentWeapon.WeaponIdx);
        WeaponSelectUIInit();
        AllUIOff();
    }
    
    public void ShowPauseUI(bool ShowUI)
    {
        _pauseUI.SetActive(ShowUI);
        _pauseUIOn = ShowUI;
        if (ShowUI)
        {
            GenericSingleton<GameManager>.Instance.SetGameState(GameManager.GameState.Paused);
        }
        else
        {
            GenericSingleton<GameManager>.Instance.SetGameState(GameManager.GameState.InGame);
        }
    }
    public void ShowWeaponSelectUI(bool ShowUI)
    {
        _weaponSelectUI.SetActive(ShowUI);
        _weaponOn = ShowUI;
        if (ShowUI)
        {
            GenericSingleton<GameManager>.Instance.SetGameState(GameManager.GameState.Paused);
        }
        else
        {
            GenericSingleton<GameManager>.Instance.SetGameState(GameManager.GameState.InGame);
        }
    }

    public void ShowExchangeUI(bool ShowUI)
    {
        _exchangeUI.SetActive(ShowUI);
        _exchangeOn = ShowUI;
        if (ShowUI)
        {
            GenericSingleton<GameManager>.Instance.SetGameState(GameManager.GameState.Paused);
        }
        else
        {
            GenericSingleton<GameManager>.Instance.SetGameState(GameManager.GameState.InGame);
        }
    }
    public void ShowPlayerUI(bool ShowUI)
    {
        _playerUI.SetActive(ShowUI);
    }
    public void ShowWarningUI(bool isShow)                         //데미지 받을때 UI
    {
        _warningUI.SetActive(isShow);
    }
    public void ShowGameOverUI(bool isShow)
    {
        _gamoverUI.SetActive(isShow);
    }
    public void AllUIOff()                                         //모든 UI끄기
    {
        ShowExchangeUI(false);
        ShowWeaponSelectUI(false);
        ShowPlayerUI(false);
        ShowPauseUI(false);
        ShowGameOverUI(false);
    }    
    public void InvenWeaponOff()
    {
        ShowExchangeUI(false);
        ShowWeaponSelectUI(false);
    }
    public void Fadeout(bool isShow)
    {
        _fadeOut.GetComponent<FadeOut>().enabled = true;
        _fadeOut.SetActive(isShow);
        
    }
    public void HpUIInit()
    {
        _HPBarUI.GetComponent<HpUI>().Init();
    }
    public void InventoryInit(Dictionary<int, ItemData> InvenData)  //인벤 갱신
    {
        _inventoryUI.GetComponent<Inventory>().ReDrwing(InvenData);
    }
    public void ExchangeUIInit()                                  //거래 UI 갱신
    {
        _exchangeUI.GetComponent<ExchangeUI>().Init();
    }
    public void SetCurrentBullet(int currentBullet)                //무기UI 갱신(현재 총알)
    {
        _weaponUI.GetComponent<WeaponUI>().SetCurrentBullet(currentBullet);
    }
    public void WeaponSelectUIInit()              //무기 선택 UI갱신
    {
        _weaponSelectUI.GetComponent<WeaponSelectUI>().Init();
    }
    public void WeaponSelectUIUnlock(WeaponBase weapon)              //무기 선택 UI갱신
    {
        _weaponSelectUI.GetComponent<WeaponSelectUI>().WeaponUnlock(weapon.WeaponIdx - 2);
    }
    public void WeaponUIInit(WeaponBase weapon)                    //무기UI 갱신
    {
        _weaponUI.GetComponent<WeaponUI>().UIUpdate(weapon);
    }
    public void SetCrosshair(int idx)
    {
        _crossHairs.GetComponent<CrossHairUI>().SetCrosshairs(idx);
    }
    public void CrossHairOff()
    {
        _crossHairs.GetComponent<CrossHairUI>().AllOff();
    }
    public void ShowOptionUI(bool isShow)
    {
        _pauseUI.GetComponent<PauseUI>().ShowOptionUI(isShow);
    }
    public void ShowQuitCheckUI(bool isShow)
    {
        _pauseUI.GetComponent<PauseUI>().ShowQuitCheckUI(isShow);
    }
    public void GoToMainMenu()
    {
        _pauseUI.GetComponent<PauseUI>().GoToMainMenu();
    }
    public void QuitCancle()
    {
        _pauseUI.GetComponent<PauseUI>().QuitCancle();
    }
    public void QuitGame()
    {
        _pauseUI.GetComponent<PauseUI>().QuitGame();
    }

}
