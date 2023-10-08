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
    [SerializeField] GameObject _pickUpUI;
    [SerializeField] TextMeshProUGUI _healItemUI;
    [SerializeField] GameObject _inventoryUI;
    [SerializeField] GameObject _warningUI;
    [SerializeField] GameObject _crossHairs;
    [SerializeField] GameObject _playerUI;
    [SerializeField] TextMeshProUGUI _fpsUI;
    [SerializeField] GameObject _loadGame;
    [SerializeField] Sprite[] _itemIcon;
    public Sprite[] ItemIcon { get { return _itemIcon; } }

    bool _weaponOn;
    public bool WeaponOn { get { return _weaponOn; } }
    bool _exchangeOn;
    public bool ExchangeOn { get { return _exchangeOn;} }
    bool _pauseUIOn;
    public bool PauseUIOn { get { return _pauseUIOn; } }

    public delegate void SliderUI(float value);

    // 각각의 델리게이트 인스턴스
    public SliderUI MasterVolume;
    public SliderUI EffectVolume;
    public SliderUI MusicVolume;
    public SliderUI MouseSense;

   

    public void MasterSoundSlider(float value)
    {
        if (MasterVolume != null)
        {
            MasterVolume(value);
        }
    }
    public void EffectSoundSlider(float value)
    {
        if (EffectVolume != null)
        {
            EffectVolume(value);
        }
    }
    public void MusicSoundSlider(float value)
    {
        if (MusicVolume != null)
        {
            MusicVolume(value);
        }
    }
    public void MouseSlider(float value)
    {
        if (MouseSense != null)
        {
            MouseSense(value);
        }
    }

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
    public void HealItemInit()
    {
        foreach(KeyValuePair<int,ItemData> inven in GenericSingleton<ItemSaver>.Instance.Datas._items)
        {
            if (inven.Value.ItemIdx == 1)
            {
                _healItemUI.text = inven.Value.Count.ToString();
                return;
            }
        }
        _healItemUI.text = "0";

    }
    public void Init()
    {
        _inventoryUI.GetComponent<Inventory>().Init();
        _exchangeUI.GetComponent<ExchangeUI>().Init();
        SetCrosshair();
        WeaponSelectUIInit();
        HpUIInit();
        HealItemInit();
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
            ShowOptionUI(false);
            ShowQuitCheckUI(false);
        }
    }
    void PauseUIOff()
    {
        _pauseUI.SetActive(false);
        _pauseUIOn = false  ;
        ShowOptionUI(false);
        ShowQuitCheckUI(false);
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
    public void ShowPlayerUI(bool ShowUI)                         //플레이어 UI
    {
        _playerUI.SetActive(ShowUI);
    }
    public void ShowPickUpUI(bool ShowUI)                         //픽업 UI
    {
        _pickUpUI.SetActive(ShowUI);
    }
    public void ShowWarningUI(bool isShow)                         //데미지 받을때 UI
    {
        _warningUI.SetActive(isShow);
    }
    public void ShowGameOverUI(bool isShow)                       //게임오버 UI
    {
        _gamoverUI.SetActive(isShow);
    }
    public void ShowLodingSceneUI(bool isShow)
    {
        _loadGame.SetActive(isShow);
    }
    public void AllUIOff()                                         //모든 UI끄기
    {
        ShowExchangeUI(false);
        ShowWeaponSelectUI(false);
        ShowPlayerUI(false);
        ShowPauseUI(false);
        ShowGameOverUI(false);
        ShowLodingSceneUI(false);
    }    
    public void InvenWeaponOff()
    {
        ShowExchangeUI(false);
        ShowWeaponSelectUI(false);
    }
    public void HpUIInit()
    {
        _HPBarUI.GetComponent<HpUI>().Init();
    }
    public void InventoryInit()  //인벤 갱신
    {
        _inventoryUI.GetComponent<Inventory>().ReDrwing(GenericSingleton<ItemSaver>.Instance.Datas._items);
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
    public void SetCrosshair()                                        //크로스헤어 갱신
    {
        _crossHairs.GetComponent<CrossHairUI>().SetCrosshairs();
    }
    public void CrossHairOff()                                      //크로스헤어 끄기
    {
        _crossHairs.GetComponent<CrossHairUI>().AllOff();
    }
    public void ShowOptionUI(bool isShow)                             //옵션창 
    {
        _pauseUI.GetComponent<PauseUI>().ShowOptionUI(isShow);
    }
    public void ShowQuitCheckUI(bool isShow)                        //게임종료 확인창 
    {
        _pauseUI.GetComponent<PauseUI>().ShowQuitCheckUI(isShow);
    }
    public void GoToMainMenu()                                        //메인메뉴 가기
    {
        ShowWarningUI(false);
        ShowGameOverUI(false);
        PauseUIOff();
        _pauseUI.GetComponent<PauseUI>().GoToMainMenu();
    }
    public void QuitCancle()                                        //게임 종료 취소
    {
        _pauseUI.GetComponent<PauseUI>().QuitCancle();
    }
    public void QuitGame()                                            //게임 종료 
    {
        _pauseUI.GetComponent<PauseUI>().QuitGame();
    }

}
