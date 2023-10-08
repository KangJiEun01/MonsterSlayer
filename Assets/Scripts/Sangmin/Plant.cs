using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    void TurnOn()
    {
        gameObject.SetActive(true);
    }
    public void TurnOffAndOn(float delay)
    {
        Invoke("TurnOn", delay);
        GenericSingleton<ItemSaver>.Instance.AddItem(new ItemData(0, 1));
        gameObject.SetActive(false);
    }
}
