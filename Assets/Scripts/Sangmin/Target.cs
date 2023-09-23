using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float Hp;
    [SerializeField] float damageDelay = 2f;
    Animator animator;
    bool inDamage;
    
    public bool InDamage { get { return inDamage; } }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnDamage(float damage)
    {
        inDamage= true;
        Hp -= damage;
        Debug.Log("��������");
        if (Hp  <= 0)
        {
            DieAni();
        }
        Invoke("DamageEnd", damageDelay);
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
    void DamageEnd()
    {
        inDamage= false;
    }
    public float GetHP()
    {
        return Hp;
    }
}
