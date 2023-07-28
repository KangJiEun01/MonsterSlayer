using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Attack02 : MonoBehaviour
{
    [SerializeField] GameObject player02;
    [SerializeField] GameObject camera;

    float BossSpeed = 5;
    private void OnEnable()
    {
        GetComponent<Animator>().Play("2_Atk2");
        Invoke("CameraMove", 1.5f);
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

        Vector3 playerVector = new Vector3(player02.transform.position.x, 0, player02.transform.position.z);
        transform.LookAt(player02.transform);
        if (Vector3.Distance(player02.transform.position, transform.position) > 10f) //Y축 빼고 따라오게 바꾸기 new Ve3
        {
            transform.position = Vector3.MoveTowards(transform.position, playerVector, BossSpeed * Time.deltaTime);
        }
        //if (Vector3.Distance(player02.transform.position, transform.position) > 10f)
        //{
        // transform.position = Vector3.MoveTowards(transform.position, player02.transform.position, BossSpeed * Time.deltaTime);
        //}
    }
    void CameraMove()
    {
        camera.GetComponent<NewCameraShake>().enabled = true;
    }
}
