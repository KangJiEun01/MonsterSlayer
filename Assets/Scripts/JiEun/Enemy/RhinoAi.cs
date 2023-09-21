using UnityEngine;
using UnityEngine.Pool;

public class RhinoAi : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject detectionUi;
    [SerializeField] float RandposXmin;
    [SerializeField] float RandposXmax;
    [SerializeField] float RandposZmin;
    [SerializeField] float RandposZmax;

    Animator rhinoAni;
    Rigidbody rhinoRigidbody;

    float moveSpeed = 1.0f; // 속도
    float changeInterval = 3.0f; // 목표지점 변경주기
    float maxangle = 50.0f;

    private Vector3 targetPosition; // 현재 목표지점
    //private float timer = 0.0f;

    private void Awake()
    {
        rhinoAni = GetComponent<Animator>();
        rhinoRigidbody = GetComponent<Rigidbody>();
        detectionUi.SetActive(false);

        rhinoRigidbody.freezeRotation = true;
    }
    private void Start()
    {
        //SetRandomTargetPosition();
        //rhinoAni.Play("Walk");
    }
    private void OnEnable()
    {
        detectionUi.SetActive(false);
        SetRandomTargetPosition();
        rhinoAni.Play("Walk");
    }

    private void Update()
    {
        transform.LookAt(transform.forward);
        //transform.forward = Vector3.forward;
        // 목표지점이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // 도착하면 다음 목표지점
            if (Vector3.Distance(transform.position, targetPosition) < 0.8f)
            {
            Debug.Log("포효중");
            GetComponent<RhinoShout>().enabled = true;
            GetComponent<RhinoAi>().enabled = false;
            //rhinoAni.Play("shout");
            //float time = Random.Range(2.0f, 4.0f);
            //Invoke("SetRandomTargetPosition", 3.4f);
            SetRandomTargetPosition();
        }
            else if(Vector3.Distance(transform.position, player.transform.position) < 10f)
        {
            transform.LookAt(player.transform);
            detectionUi.SetActive(true);
            RunTrue();
        }    
    }
    //private void FixedUpdate()
    //{
    //    RaycastHit hit;
    //    if(Physics.Raycast(transform.position, Vector3.down, out hit)) //올라가는 경사 
    //    {
    //        float angle = Vector3.Angle(hit.normal, Vector3.up);
    //        if(angle > maxangle)
    //        {
    //            rhinoRigidbody.velocity = Vector3.zero;
    //        }
    //    }
    //}
    private void SetRandomTargetPosition()
    {
        Debug.Log("순찰중");
        // 랜덤포지션
        float x = Random.Range(RandposXmin, RandposXmax);
        float z = Random.Range(RandposZmin, RandposZmax);
        targetPosition = new Vector3(x, 4.0f, z);
        //transform.LookAt(transform.forward);
        transform.LookAt(targetPosition); //상태전이할때도 같은 방향 보게
    }
    void RunTrue()
    {
       // detectionUi.SetActive(false);
        GetComponent<RhinoRun>().enabled = true;
        GetComponent<RhinoAi>().enabled = false;
    }
    public void targetPos(Vector3 pos)
    {
        pos = targetPosition;
    }
}
