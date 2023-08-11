using UnityEngine;

public class Enemy01Gun : MonoBehaviour
{
    bool attack = false;
    void Start()
    {
        
    }
    void Update()
    {
        //attack = GetComponent<Enemy01controller>().Attacker;
       if(attack)
        {
            Debug.Log("°ø°ÝÁß");
        }
    }
}
