using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Attack02 : MonoBehaviour
{
    [SerializeField] GameObject player02;
    [SerializeField] GameObject camera;
    private void OnEnable()
    {
        GetComponent<Animator>().Play("3_Atk2");
        //Invoke("CameraMove", 1.6f);
        //Invoke("EnabledFalse", 1.8f);
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void CameraMove()
    {
        camera.GetComponent<NewCameraShake>().enabled = true;
    }
}
