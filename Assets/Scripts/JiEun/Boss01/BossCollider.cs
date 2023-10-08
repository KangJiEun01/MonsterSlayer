using UnityEngine;

public class BossCollider : MonoBehaviour
{
    float _playerHp;
    [SerializeField] GameObject camera;
   

    private void Start()
    {
        _playerHp = GenericSingleton<PlayerCon>.Instance.GetComponent<PlayerCon>().HpStat;
        //camera = Camera.main;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("player"))
        {
            CameraMove();
            RedWaring();
            Debug.Log("Ä«¸Þ¶ó");
        }
    }
    void CameraMove()
    {
        camera.GetComponent<NewCameraShake>().enabled = true;
    }
    void RedWaring()
    {
        GenericSingleton<UIBase>.Instance.ShowWarningUI(true);
        Invoke("RedWaringActFalse", 0.3f);
    }
    void RedWaringActFalse()
    {
        GenericSingleton<UIBase>.Instance.ShowWarningUI(false);
    }
}
