
using UnityEngine;

public class Boss03Attack03 : MonoBehaviour
{ 
    [SerializeField] GameObject player02;
    [SerializeField] GameObject camera;
private void OnEnable()
{
    GetComponent<Animator>().Play("3_Atk3");
    Invoke("CameraMove", 1.6f);
    //Invoke("EnabledFalse", 1.8f);
}
void Start()
{

}

void Update()
{

}
void CameraMove()
{
    camera.GetComponent<NewCameraShake>().enabled = true;
}
void EnabledFalse()
{
    //GetComponent<Boss02Attack01>().enabled = false;
}
}