using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairUI : MonoBehaviour
{
    [SerializeField] GameObject[] _crosshairs;
    public void SetCrosshairs()
    {
        AllOff();
        _crosshairs[GenericSingleton<WeaponManager>.Instance.CurrentWeapon.WeaponIdx].SetActive(true);
    }
    public void AllOff()
    {
        foreach (var c in _crosshairs)
        {
            c.gameObject.SetActive(false);
        }
    }
}
