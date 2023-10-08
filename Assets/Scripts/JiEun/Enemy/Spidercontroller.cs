using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;
using System.Collections;


public class Spidercontroller : MonoBehaviour
{
    public VisualEffect VFXGraph;
    public SkinnedMeshRenderer skinnedMesh;
    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;
    [SerializeField] private Material[] skinnedMaterials;

    //[SerializeField] GameObject deathEffect;
    GameObject player;
    Collider spider;
    NavMeshAgent agent;
    Transform spiderpos;
    Animation Ani;
    Coroutine _co;
    float _hp;
    float timer = 4;

    bool attack = false;
    bool spiderDeath = false;
    void Start()
    {
        spider= GetComponent<Collider>();
        if (skinnedMesh != null)
        {
            skinnedMaterials = skinnedMesh.materials;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        agent = GetComponent<NavMeshAgent>();
        spiderpos = GetComponent<Transform>();
        Ani = GetComponent<Animation>();
        Ani.Play("walk");
    }

    void Update()
    {
        _hp = GetComponent<Target>().Hp;
        if (_hp <= 0)
        {
            spiderDeath = true;
            spider.enabled = false;
            //agent.velocity = Vector3.zero;
            agent.isStopped = true; //네비 움직임 멈춤
            //deathEffect.SetActive(true);
            _co = StartCoroutine(DissolveCo());
            Transform childPrefab = transform.Find("VFXGraph_CharacterDissolve"); // "ChildPrefabName"을 프리팹의 이름으로 바꿔주세요.
            if (childPrefab != null)
            {
                childPrefab.gameObject.SetActive(true);
            }
            Ani.Play("death1");
            Invoke("Death", 1.8f);
        }
        if (!spiderDeath)
        {
            if (_hp > 0 && !attack && !GetComponent<Target>().InDamage)
            {
                timer += Time.deltaTime;
                if (timer > 4)
                {
                    Ani.Play("run");//걷는 모션 수정
                    timer = 0;
                    SetRandomTargetPosition();
                    if (Vector3.Distance(transform.position, player.transform.position) < 5f)
                    {
                        attack = true;
                    }
                }
            }
            if (_hp > 0 && attack && !GetComponent<Target>().InDamage)
            {
                Ani.Play("run");
                agent.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
                if (Vector3.Distance(transform.position, player.transform.position) < 3f)
                {
                    //Ani.Play("attack2");
                    Invoke("walk", 2f);
                }
            }
            if (_hp > 0 && GetComponent<Target>().InDamage)
            {
                Ani.Play("hit1");
            }
        }
    }
    private void SetRandomTargetPosition()
    {
        float x = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        agent.SetDestination(new Vector3(spiderpos.position.x - x, spiderpos.position.y, spiderpos.position.z - z));
        //transform.LookAt(transform.forward);
    }
    void walk()
    {
        Ani.Play("walk");
        attack = false;
    }
    void Death()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false); //오류****
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }
    void RedWaringActFalse()
    {
        GenericSingleton<UIBase>.Instance.ShowWarningUI(false);
    }

    IEnumerator DissolveCo()
    {
        if (VFXGraph != null)
        {
            VFXGraph.Play();
        }

        if (skinnedMaterials.Length > 0)
        {
            float counter = 0;

            while (skinnedMaterials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for (int i = 0; i < skinnedMaterials.Length; i++)
                {
                    skinnedMaterials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
