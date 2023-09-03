using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : GenericSingleton<UIBase>
{
    [SerializeField] GameObject _invenCheckUI;
    [SerializeField] GameObject _runToggle;
    [SerializeField] GameObject _inventory;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ShowInvenCheckUI(bool ShowUI) => _invenCheckUI.SetActive(ShowUI);
   

    public void ShowInvenUI(bool ShowUI) => _inventory.SetActive(ShowUI);

}
