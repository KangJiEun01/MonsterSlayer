using UnityEngine;

public class BossCollider : MonoBehaviour
{
    float _playerHp;
    [SerializeField] GameObject camera;
    GameObject redWaring;

    private void Start()
    {
        redWaring = GenericSingleton<UIBase>.Instance.WarningUI;
        _playerHp = GenericSingleton<PlayerCon>.Instance.GetComponent<PlayerCon>().HpStat;
        //camera = Camera.main;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
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
        redWaring.SetActive(true);
        Invoke("RedWaringActFalse", 0.3f);
    }
    void RedWaringActFalse()
    {
        redWaring.SetActive(false);
    }
}
