using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunToggleUI : MonoBehaviour
{
    [SerializeField] Image _backImage;
    void Update()
    {
        _backImage.fillAmount = GenericSingleton<PlayerCon>.Instance.RunTimer / 2;
    }
}
