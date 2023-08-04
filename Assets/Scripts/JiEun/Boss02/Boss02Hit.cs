using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Hit : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().Play("2_Hit");
        Invoke("EnabledFalse", 0.5f);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void EnabledFalse()
    {
        GetComponent<Boss02Hit>().enabled = false;
    }

}
