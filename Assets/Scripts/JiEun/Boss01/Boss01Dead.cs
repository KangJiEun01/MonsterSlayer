
using UnityEngine;

public class Boss01Dead : MonoBehaviour
{
    [SerializeField] GameObject Boss2;
    void Start()
    {
        Boss2.transform.position = transform.position;
    }
    private void OnEnable()
    {
        GetComponent<Animator>().Play("Dead");//애니메이션 다 플레이하고 사라지게 변경
        Invoke("Boss1Dead", 7f);
    }
    void Update()
    {

    }
    void Boss1Dead()
    {
        Destroy(gameObject);
        Boss2.GetComponent<Boss02NewAi>().enabled = true;
        Boss2.SetActive(true);
    }
    public Transform Boss1TransBoss1Trans()
    {
        return transform;
    }
}
