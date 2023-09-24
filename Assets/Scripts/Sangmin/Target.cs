using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float Hp;
    [SerializeField] float damageDelay = 2f;
    [SerializeField] GameObject[] DropItem;
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
        Debug.Log("피해입음");
        if (Hp  == 0)
        {
            DieAni();
        }
        Invoke("DamageEnd", damageDelay);
    }
    void DieAni()
    {
        animator.Play("Die"); //이름 맞춰주기
        Invoke("Die", 2f);
    }
    void Die()
    {
        int num = Random.Range(0, 4);
        //for(int i = 0; i < 1; i++) 
        //{
            Instantiate(DropItem[num], new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z), Quaternion.identity);
            //i++;
        //}
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
