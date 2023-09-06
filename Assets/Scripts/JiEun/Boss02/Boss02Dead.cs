
using UnityEngine;

public class Boss02Dead : MonoBehaviour
{
    [SerializeField] GameObject Boss03;

    private void OnEnable()
    {
        GetComponent<Animator>().Play("2_Idle");
        Boss03.transform.position = transform.position;
        Invoke("Boss2Dead", 3f);
    }
    void Boss2Dead()
    {
        Destroy(gameObject);
        Boss03.GetComponent<Boss03NewAi>().enabled = true;
        Boss03.SetActive(true);
    }
    void Start()
    {
        
    }
    void Update()
    {

    }
}
