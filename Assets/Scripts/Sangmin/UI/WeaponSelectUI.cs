using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour
{
    [SerializeField] GameObject[] _buttons;
    [SerializeField] GameObject[] _locks;
    [SerializeField] GameObject[] _redButtons;
    [SerializeField] GameObject[] _blackMasks;
    [SerializeField] Sprite _blueImage;
    [SerializeField] Sprite _defaultImage;

    public void WeaponUnlock(int idx)
    {
        _locks[idx].SetActive(false);
        _redButtons[idx].SetActive(false);
        _blackMasks[idx].SetActive(false);
    }
    public void SetMainWeapon(int idx)
    {
        AllOff();
        _buttons[idx-2].GetComponent<Button>().interactable = false;
        _buttons[idx-2].GetComponent<Image>().sprite= _blueImage;
        GenericSingleton<WeaponManager>.Instance.SetMainWeapon(idx);
    }
    void AllOff()
    {
        foreach (var button in _buttons)
        {
            button.GetComponent<Button>().interactable = true;
            button.GetComponent<Image>().sprite = _defaultImage;
        }
    }
}
