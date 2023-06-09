using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    Animator _auto;


    // Start is called before the first frame update
    void Start()
    {
        _auto = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _auto.SetBool("isOpen", true);

    }

    private void OnTriggerExit(Collider other)
    {
        _auto.SetBool("isOpen", false);

    }
}
