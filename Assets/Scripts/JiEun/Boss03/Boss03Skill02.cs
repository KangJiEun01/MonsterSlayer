using UnityEngine;

public class Boss03Skill02 : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().Play("3_Skill2");
        Invoke("EnabledFalse", 3.00f);
    }
    void EnabledFalse()
    {
        GetComponent<Boss03Skill02>().enabled = false;
    }
}
