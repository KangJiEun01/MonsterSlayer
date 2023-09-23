using UnityEngine;

public class FireBullet : MonoBehaviour
{
    GameObject camera;
    //GameObject redWaring;

    Vector3 _dir;
    float _speed;
    private void Start()
    {
        camera = Camera.main.gameObject;
        //redWaring = GenericSingleton<UIBase>.Instance.WarningUI;
    }
    void Update()
    {
        transform.position += _dir * Time.deltaTime * _speed;
    }
    public void Init(Vector3 dir, float speed)
    {
        _dir = dir;
        _speed = speed;
        Invoke("DeleteBullet", 2f);
    }
    void DeleteBullet()
    {
        Destroy(gameObject);
    }
    void CameraMove()
    {
        camera.GetComponent<NewCameraShake>().enabled = true;
    }
    void RedWaring()
    {
       // redWaring.SetActive(true);
       // Invoke("RedWaringActFalse", 0.3f);
    }
    void RedWaringActFalse()
    {
      //  redWaring.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("카메라 흔들기");
            CameraMove();
            //RedWaring();
        }
        DeleteBullet();
    }
}
