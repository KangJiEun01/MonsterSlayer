using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] public float Hp;
    [SerializeField] float damageDelay = 2f;
    [SerializeField] GameObject[] DropItem;
    
    bool inDamage;
    
    public bool InDamage { get { return inDamage; } }
    public void OnDamage(float damage)
    {
        if (Hp > 0)
        {
            inDamage = true;
            Hp -= damage;
            Invoke("DamageEnd", damageDelay);
        }
    }
    void Die()
    {
        int num = Random.Range(0, 4);
        Instantiate(DropItem[num], new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z), Quaternion.identity);
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
