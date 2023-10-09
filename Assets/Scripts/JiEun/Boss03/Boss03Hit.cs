using UnityEngine;

public class Boss03Hit : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().Play("3_Hit");
        Invoke("EnabledFalse", 0.1f);
    }
    void EnabledFalse()
    {
        GetComponent<Boss03Hit>().enabled = false;
    }
}
