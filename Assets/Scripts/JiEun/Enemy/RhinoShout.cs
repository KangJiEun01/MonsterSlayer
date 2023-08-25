using TMPro;
using UnityEngine;

public class RhinoShout : MonoBehaviour
{
    Animator rhinoAni;
    private void Awake()
    {
        rhinoAni = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        transform.LookAt(transform.forward);
        int RandAni = Random.Range(0, 2);
            if(RandAni ==0)
        {
            rhinoAni.Play("shout");
        }
        else
        {
            rhinoAni.Play("Eats");
        }
        Invoke("RhinoIdle", 3.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RhinoIdle()
    {
        GetComponent<RhinoShout>().enabled = false;
        GetComponent<RhinoAi>().enabled = true;
    }
}
