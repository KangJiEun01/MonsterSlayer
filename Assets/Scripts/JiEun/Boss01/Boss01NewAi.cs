using System.Collections;
using UnityEngine;

public class Boss01NewAi : MonoBehaviour
{
    [SerializeField] GameObject bossMat;
    //[SerializeField] AudioClip bossAudio;
    //AudioSource audioSource;
    Renderer bossRend;
    GameObject player;
    Animator animator;

    public float BossHp;
    //float PlayerHP = GenericSingleton<PlayerCon>.Instance.GetComponent<PlayerCon>().HpStat;
    bool startAttack = false;
    bool Mode = false; //1, 2 공격모드
    public bool _attack = false; //공격on, off상태
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bossRend = bossMat.GetComponent<Renderer>();
        transform.LookAt(player.transform);
        BossHp = GetComponent<Target>().GetHP();
        animator = GetComponent<Animator>();
        //audioSource=GetComponent<AudioSource>();
        animator.Play("In");
        Invoke("StartRout", 4.7f);
    }
    private IEnumerator BossAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            StartAttack();

            yield return new WaitForSeconds(3f);
            GetComponent<BossAttack02>().enabled = false;
            GetComponent<BossAttack01>().enabled = false;
        }
    }
    private void StartAttack()
    {
        if (BossHp > 0)
        {
            if (Mode == false) //2번 공격
            {
                if (_attack == false)
                {
                    _attack = true;
                    Atk02();
                    _attack = false;
                }
                Mode = true;
            }
            else if (Mode == true)//1번 공격 뒤 돌게 모션
            {
                if (_attack == false)
                {
                    _attack = true;
                    Atk01();
                    _attack = false;
                }
                Mode = false;
            }
        }
    }
    void Update()
    {
        if (startAttack)
        {
            float BossHp = GetComponent<Target>().GetHP();
            if (BossHp <= 0)
            {
                GetComponent<Boss01Dead>().enabled = true;
                GetComponent<Boss01NewAi>().enabled = false;
                GetComponent<BossAttack01>().enabled = false;
                GetComponent<BossAttack02>().enabled = false;
                GetComponent<BossSkill01>().enabled = false;
                StopCoroutine(_co);
            }
            else if (BossHp > 0 && GetComponent<Target>().InDamage)
            {
                bossRend.material.color = Color.gray;
                Invoke("MatColor", 0.3f);
            }
        }
    }
    void MatColor()
    {
        bossRend.material.color = Color.white;
    }
    void Atk02()
    {
        //Invoke("AttackSound",1f);
        //audioSource.PlayOneShot(bossAudio);
        GetComponent<BossAttack02>().enabled = true;
    }
    void AttackSound()
    {
        //audioSource.PlayOneShot(bossAudio);
    }
    void Atk01()
    {
        GetComponent<BossAttack01>().enabled = true;
    }
    public float getBossHP()
    {
        return BossHp;
    }
    Coroutine _co;
    void StartRout()
    {
        startAttack = true;
        _co = StartCoroutine(BossAttackRoutine());
    }
}


