using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairUI : MonoBehaviour
{
    [SerializeField] GameObject[] _crosshairs;
    public void SetCrosshairs(int idx)
    {
        Init();
        _crosshairs[idx].SetActive(true);
    }
    public void Init()
    {
        foreach (var c in _crosshairs)
        {
            c.gameObject.SetActive(false);
        }
    }
}
