using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : GenericSingleton<UIBase>
{
    [SerializeField] GameObject _invenCheckUI;
    [SerializeField] GameObject _runToggle;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OpenInvenCheckUI()
    {
        _invenCheckUI.SetActive(true);
    }
    public void CloseInvenCheckUI()
    {
        _invenCheckUI?.SetActive(false);
    }
    public void OpenRunToggleUI()
    {
        _runToggle.SetActive(true);
    }
    public void CloseRunToggleUI()
    {
        _runToggle?.SetActive(false);
    }
}
