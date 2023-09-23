using UnityEngine;

public class UIBase : GenericSingleton<UIBase>
{
    [SerializeField] GameObject _weaponUI;
    public GameObject WeaponUI { get { return _weaponUI; } }
    [SerializeField] GameObject _weaponSelectUI;
    public GameObject WeaponSelectUI { get { return _weaponSelectUI; } }

    [SerializeField] GameObject _exchangeUI;
    public GameObject ExchangeUI { get { return _exchangeUI; } }

    [SerializeField] GameObject _runToggleUI;
    public GameObject RunToggleUI { get { return _runToggleUI; } }

    [SerializeField] GameObject _inventoryUI;
    public GameObject InventoryUI { get { return _inventoryUI; } }
    [SerializeField] GameObject _warningUI;
    public GameObject WarningUI { get { return _warningUI; } }

    bool _weaponOn;
    public bool WeaponOn { get { return _weaponOn; } }
    bool _exchangeOn;
    public bool ExchangeOn { get { return _exchangeOn;} }
    public void Init()
    {
        _inventoryUI.GetComponent<Inventory>().Init();
        _exchangeUI.GetComponent<ExchangeUI>().Init();
        AllUIOff();
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
    public void ExchangeOff()
    {
        _exchangeUI.SetActive(false);
        _exchangeOn = false;
        GenericSingleton<GameManager>.Instance.SetGameState(GameManager.GameState.InGame);
    }
    public void WeaponSelectOff()
    {
        _weaponSelectUI.SetActive(false);
        _weaponOn = false;
        GenericSingleton<GameManager>.Instance.SetGameState(GameManager.GameState.InGame);
    }
    public void AllUIOff()
    {
        ShowExchangeUI(false);
        ShowWeaponSelectUI(false);
    }
}
