using UnityEngine;

public class Boss03Skill02 : MonoBehaviour
{
    GameObject player;
    GameObject camera;
    private void OnEnable()
    {
        GetComponent<Animator>().Play("3_Skill2");
        //Invoke("CameraMove", 1.6f);
        //Invoke("EnabledFalse", 1.8f);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = Camera.main.gameObject;
    }
    void Update()
    {
        
    }
    void CameraMove()
    {
        camera.GetComponent<NewCameraShake>().enabled = true;
    }
}
