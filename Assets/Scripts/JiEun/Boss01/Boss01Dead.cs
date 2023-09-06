
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
        GetComponent<Animator>().Play("Dead");//�ִϸ��̼� �� �÷����ϰ� ������� ����
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
