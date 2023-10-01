using UnityEngine;

public class Boss03Attack03 : MonoBehaviour
{
    GameObject player;
    GameObject camera;
    private void OnEnable()
    {
    GetComponent<Animator>().Play("3_Atk3");
    Invoke("EnabledFalse", 2.0f);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = Camera.main.gameObject;
    }
    void EnabledFalse()
    {
    GetComponent<Boss03Attack03>().enabled = false;
    }
}