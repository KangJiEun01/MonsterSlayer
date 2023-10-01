using UnityEngine;

public class Boss03Attack01 : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().Play("3_Atk1");
        Invoke("EnabledFalse", 3.19f);
    }
    void EnabledFalse()
    {
        GetComponent<Boss03Attack01>().enabled = false;
    }
}
