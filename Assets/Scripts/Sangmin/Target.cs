using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] public float Hp;
    [SerializeField] float damageDelay = 2f;
    
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
    void DamageEnd()
    {
        inDamage= false;
    }
    public float GetHP()
    {
        return Hp;
    }
}
