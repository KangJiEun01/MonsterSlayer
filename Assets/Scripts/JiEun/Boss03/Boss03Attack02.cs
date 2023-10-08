using UnityEngine;

public class Boss03Attack02 : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos;
    [SerializeField] float bulletSpeed;

    Vector3 VectorbulletPos;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
        VectorbulletPos = bulletPos.position;
    }
    private void OnEnable()
    {
        GetComponent<Animator>().Play("3_Atk2");
        Invoke("BulletFire", 1f);
        Invoke("EnabledFalse", 4.24f);
    }
    void BulletFire()
    {
        //Vector3 attackst = new Vector3(VectorbulletPos.x + (-2.0f), VectorbulletPos.y+(+5.0f), VectorbulletPos.z + (-8.5f));
       // Vector3 attackst = new Vector3(VectorbulletPos.x, VectorbulletPos.y, VectorbulletPos.z); ;
        GameObject temp = Instantiate(bullet);
        //Vector3 worldPosition = bulletPos.TransformPoint(Vector3.zero);
        //Vector3 worldPosition = bulletPos.TransformPoint(attackst);
        //temp.transform.position = worldPosition;
        temp.transform.position = new Vector3(bulletPos.position.x, bulletPos.position.y+10f, bulletPos.position.z);
        Vector3 dir =player.transform.position; 
        //Vector3 dir = transform.forward; //æ’πÊ«‚
        temp.GetComponent<Boss03Bullet>().Init(dir, bulletSpeed);
    }
    void EnabledFalse()
    {
        GetComponent<Boss03Attack02>().enabled = false;
    }
}
