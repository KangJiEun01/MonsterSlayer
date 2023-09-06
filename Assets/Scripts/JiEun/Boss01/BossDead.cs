
using UnityEngine;

public class BossDead : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().Play("Dead");
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
