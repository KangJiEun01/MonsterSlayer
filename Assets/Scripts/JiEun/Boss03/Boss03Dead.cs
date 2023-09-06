
using UnityEngine;

public class Boss03Dead : MonoBehaviour
{

    private void OnEnable()
    {
        GetComponent<Animator>().Play("Dead");
        //Invoke("CameraMove", 1.6f);
        //Invoke("EnabledFalse", 1.8f);
        Invoke("Boss3Dead", 2f);
    }
    void Boss3Dead()
    {
        Destroy(gameObject);
    }
}
