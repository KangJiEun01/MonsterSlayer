using UnityEngine;

public class UIBase : GenericSingleton<UIBase>
{
    [SerializeField] GameObject _weaponUI;
    public GameObject WeaponUI { get { return _weaponUI; } }

    [SerializeField] GameObject _exchangeUI;
    public GameObject ExchangeUI { get { return _exchangeUI; } }

    [SerializeField] GameObject _runToggleUI;
    public GameObject RunToggleUI { get { return _runToggleUI; } }

    [SerializeField] GameObject _inventoryUI;
    public GameObject InventoryUI { get { return _inventoryUI; } }
    public void Init()
    {
        _inventoryUI.GetComponent<Inventory>().Init();
        _exchangeUI.GetComponent<ExchangeUI>().Init();
        ShowExchangeUI(false);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ShowWeaponUI(bool ShowUI) => _weaponUI.SetActive(ShowUI);
   

    public void ShowExchangeUI(bool ShowUI) => _exchangeUI.SetActive(ShowUI);
    public void ExchangeOff()
    {
        _exchangeUI.SetActive(false);
        GenericSingleton<GameManager>.Instance._exchangeUI = false;
        GenericSingleton<GameManager>.Instance.SetGameState(GameManager.GameState.InGame);
    }
}
