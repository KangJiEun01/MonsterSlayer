using UnityEngine;

public class RhinoAttack : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject camera;

    Animator rhinoAni;
    float moveSpeed = 4.0f;
    Vector3 targetPosition;

    private void Awake()
    {
        rhinoAni = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        rhinoAni.Play("Attack");
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 10f)
        {
            RhinoIdle();
        }
    }
    void RhinoIdle()
    {
        GetComponent<RhinoAttack>().enabled = false;
        GetComponent<RhinoAi>().enabled = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Ä«¸Þ¶ó");
            CameraMove();
        }
    }
    void CameraMove()
    {
        camera.GetComponent<NewCameraShake>().enabled = true;
    }
}
