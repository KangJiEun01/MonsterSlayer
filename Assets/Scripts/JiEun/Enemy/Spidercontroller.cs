using UnityEngine;
using UnityEngine.AI;
public class Spidercontroller : MonoBehaviour
{
    GameObject player;
    GameObject camera;
    GameObject redWaring;
    NavMeshAgent agent;
    Transform spiderpos;
    Animator animator;

    bool attack = false;
    void Start()
    {
        camera = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        redWaring = GenericSingleton<UIBase>.Instance.WarningUI;
        agent = GetComponent<NavMeshAgent>();
        spiderpos = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        animator.Play("walk");
    }

    float timer = 4;

    void Update()
    {
        if (!attack) 
        {
            timer += Time.deltaTime;
            if(timer > 4)
            {
                timer = 0;
                animator.Play("walk");
                SetRandomTargetPosition();
                if (Vector3.Distance(transform.position, player.transform.position) < 5f)
                {
                    attack = true;
                }
            }
        }
        if(attack)
        {
            animator.Play("run");
            agent.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
            if(Vector3.Distance(transform.position, player.transform.position) < 3f)
            {
                Debug.Log("����");
                animator.Play("attack1");
                Invoke("walk",2f);
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
        attack = false;
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
        redWaring.SetActive(true);
        Invoke("RedWaringActFalse", 0.3f);
    }
    void RedWaringActFalse()
    {
        redWaring.SetActive(false);
    }
}
