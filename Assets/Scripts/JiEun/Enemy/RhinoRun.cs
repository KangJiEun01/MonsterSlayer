using UnityEngine;

public class RhinoRun : MonoBehaviour
{
    [SerializeField] GameObject player;

    Animator rhinoAni;
    Rigidbody rhinoRigidbody;

    float maxangle = 50.0f;
    float moveSpeed = 2.0f;
    Vector3 targetPosition;

    private void Awake()
    {
        rhinoAni = GetComponent<Animator>();
        rhinoRigidbody = GetComponent<Rigidbody>();
        rhinoRigidbody.freezeRotation = true;
    }
    private void OnEnable()
    {
        rhinoAni.Play("Run");
    }
    void Update()
    {
        targetPosition = new Vector3(player.transform.position.x, 0 , player.transform.position.z);
        transform.LookAt(targetPosition);
        if (Vector3.Distance(transform.position, player.transform.position) < 10f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, player.transform.position) < 3f)
            {
                GetComponent<RhinoRun>().enabled = false;
                GetComponent<RhinoAttack>().enabled = true;
            }
        }
          if (Vector3.Distance(transform.position, player.transform.position) > 10f)
        {
            RhinoIdle();
        }
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit)) //올라가는 경사 
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            if (angle > maxangle)
            {
                rhinoRigidbody.velocity = Vector3.zero;
            }
        }
    }
    void RhinoIdle()
    {
        GetComponent<RhinoRun>().enabled = false;
        GetComponent<RhinoAi>().enabled = true;
    }
}
