
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().Play("1_Skill1"); //일부분만 반복 재생 가능?
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
