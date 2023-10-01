using UnityEngine;

public class Boss03Skill01 : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().Play("3_Skill1");
        Invoke("EnabledFalse", 3.04f);
    }
    void EnabledFalse()
    {
        GetComponent<Boss03Skill01>().enabled = false;
    }
}
