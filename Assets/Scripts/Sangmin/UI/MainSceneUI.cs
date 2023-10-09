using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField] Button _optionBttn;
    [SerializeField] Button _gameQuitBttn;


    public void Start()
    {
        _optionBttn.onClick.AddListener(OptionClick);
        _gameQuitBttn.onClick.AddListener(GameQuitClick);
    }
    void OptionClick()
    {
        GenericSingleton<UIBase>.Instance.ShowOptionUI(true);
    }
    void GameQuitClick()
    {
        GenericSingleton<UIBase>.Instance.ShowQuitCheckUI(true);
    }
}
