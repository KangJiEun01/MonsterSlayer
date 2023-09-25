using UnityEngine;

public class Boss01HpCheck : MonoBehaviour
{
    float BossHp;
    // Update is called once per frame
    private void Start()
    {
        BossHp = GetComponent<Target>().GetHP();
    }
    void Update()
    {
        BossHp = GetComponent<Target>().GetHP();
        if(BossHp ==0)
        {
            BossHp = 0;
            GetComponent<Boss01NewAi>().enabled = false;
            GetComponent<BossAttack01>().enabled = false;
            GetComponent<BossAttack02>().enabled = false;
            GetComponent<BossSkill01>().enabled = false;
            GetComponent<Boss01Dead>().enabled = true;
        }
    }
}
