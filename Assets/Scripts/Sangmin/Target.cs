using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float Hp;
    [SerializeField] Animator animator;
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
       // animator.Play("death1"); //�̸� �����ֱ�
        Invoke("Die", 2f);
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
}
