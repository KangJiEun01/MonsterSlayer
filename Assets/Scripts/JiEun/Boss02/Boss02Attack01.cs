using UnityEngine;

public class Boss02Attack01 : MonoBehaviour
{
    GameObject player;
    GameObject camera;

    float BossSpeed = 5;
    private void OnEnable()
    {
        GetComponent<Animator>().Play("2_Atk1");
        Invoke("CameraMove", 1.1f);
        Invoke("EnabledFalse", 1.8f);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        camera = Camera.main.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        //Vector3 playerVector = new Vector3(player02.transform.position.x, 0, player02.transform.position.z);
        //transform.LookAt(player02.transform);
        //if (Vector3.Distance(player02.transform.position, transform.position) > 10f) //Y축 빼고 따라오게 바꾸기 new Ve3
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, playerVector, BossSpeed * Time.deltaTime);
        //}
        //if (Vector3.Distance(player02.transform.position, transform.position) > 10f)
        //{
        // transform.position = Vector3.MoveTowards(transform.position, player02.transform.position, BossSpeed * Time.deltaTime);
        //}
    }
    void CameraMove()
    {
        camera.GetComponent<NewCameraShake>().enabled = true;
    }
    void EnabledFalse()
    {
        GetComponent<Boss02Attack01>().enabled = false;
    }
}
