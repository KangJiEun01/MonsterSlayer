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
    [SerializeField] GameObject _HPBarUI;
    public GameObject RunToggleUI { get { return _runToggleUI; } }

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

    float _timer = 0;
    int _frame = 0;
    private void Update()
    {
        CalFPS();
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
    
    public void ExchangeOff()             //�ݱ� ��ư�� ����Ǵ� �Լ�
    {
        _exchangeUI.SetActive(false);
        _exchangeOn = false;
        GenericSingleton<GameManager>.Instance.SetGameState(GameManager.GameState.InGame);
    }
    public void WeaponSelectOff()          //�ݱ� ��ư�� ����Ǵ� �Լ�
    {
        _weaponSelectUI.SetActive(false);
        _weaponOn = false;
        GenericSingleton<GameManager>.Instance.SetGameState(GameManager.GameState.InGame);
    }
    public void ShowWeaponSelectUI(bool ShowUI)
    {
        _weaponSelectUI.SetActive(ShowUI);
        _weaponOn = ShowUI;
    }

    public void ShowExchangeUI(bool ShowUI)
    {
        _exchangeUI.SetActive(ShowUI);
        _exchangeOn = ShowUI;
    }
    public void ShowPlayerUI(bool ShowUI)
    {
        _playerUI.SetActive(ShowUI);
    }
    public void ShowWarningUI(bool isShow)                         //������ ������ UI
    {
        _warningUI.SetActive(isShow);
    }
    public void GameOverUI(bool isShow)
    {
        _gamoverUI.SetActive(isShow);
    }
    public void AllUIOff()                                         //��� UI����
    {
        ShowExchangeUI(false);
        ShowWeaponSelectUI(false);
        GameOverUI(false);
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
    public void InventoryInit(Dictionary<int, ItemData> InvenData)  //�κ� ����
    {
        _inventoryUI.GetComponent<Inventory>().ReDrwing(InvenData);
    }
    public void ExchangeUIInit()                                  //�ŷ� UI ����
    {
        _exchangeUI.GetComponent<ExchangeUI>().Init();
    }
    public void SetCurrentBullet(int currentBullet)                //����UI ����(���� �Ѿ�)
    {
        _weaponUI.GetComponent<WeaponUI>().SetCurrentBullet(currentBullet);
    }
    public void WeaponSelectUIInit()              //���� ���� UI����
    {
        _weaponSelectUI.GetComponent<WeaponSelectUI>().Init();
    }
    public void WeaponSelectUIUnlock(WeaponBase weapon)              //���� ���� UI����
    {
        _weaponSelectUI.GetComponent<WeaponSelectUI>().WeaponUnlock(weapon.WeaponIdx - 2);
    }
    public void WeaponUIInit(WeaponBase weapon)                    //����UI ����
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


}
