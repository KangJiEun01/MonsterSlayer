using UnityEngine;
using UnityEngine.AI;


public class Spidercontroller : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    GameObject player;
    GameObject camera;
    NavMeshAgent agent;
    Transform spiderpos;
    Animation Ani;
    float _hp;

    bool attack = false;
    bool spiderDeath = false;
    void Start()
    {
        camera = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        agent = GetComponent<NavMeshAgent>();
        spiderpos = GetComponent<Transform>();
        Ani = GetComponent<Animation>();
        Ani.Play("walk");
    }

    float timer = 4;

    void Update()
    {
        _hp = GetComponent<Target>().Hp;
        if (_hp <= 0)
        {
            deathEffect.SetActive(true);
            spiderDeath = true;
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
                    Debug.Log("어택");
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
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            CameraMove();
            RedWaring();
        }
    }
    void CameraMove()
    {
        camera.GetComponent<NewCameraShake>().enabled = true;
    }
    void RedWaring()
    {
        GenericSingleton<UIBase>.Instance.ShowWarningUI(true);
        Invoke("RedWaringActFalse", 0.3f);
    }
    void RedWaringActFalse()
    {
        GenericSingleton<UIBase>.Instance.ShowWarningUI(false);
    }
}
