using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float Hp;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnDamage(float damage)
    {
        Hp -= damage;
        animator.Play("hit1");//�̸� �����ֱ�
        Debug.Log("��������");
        if (Hp  <= 0)
        {
            DieAni();
        }
    }
    void DieAni()
    {
        animator.Play("Die"); //�̸� �����ֱ�
        Invoke("Die", 2f);
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
    public float GetHP()
    {
        return Hp;
    }
}
