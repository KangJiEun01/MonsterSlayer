using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float Hp;
    [SerializeField] Animator animator;
    public void OnDamage(float damage)
    {
        Hp -= damage;
        Debug.Log("피해입음");
        if (Hp  <= 0)
        {
            DieAni();
        }
    }
    void DieAni()
    {
       // animator.Play("death1"); //이름 맞춰주기
        Invoke("Die", 2f);
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
}
