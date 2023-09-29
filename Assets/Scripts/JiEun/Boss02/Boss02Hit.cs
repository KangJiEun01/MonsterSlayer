using UnityEngine;

public class Boss02Hit : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().Play("2_Hit");
        Invoke("EnabledFalse", 0.04f);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void EnabledFalse()
    {
        //GetComponent<Animator>().Play("2_Run");
        GetComponent<Boss02Hit>().enabled = false;
    }

}
