
using UnityEngine;

public class BulletCon : MonoBehaviour
{
    public void Init()
    {
        Invoke("BulletDestroy",0.3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        BulletDestroy();
    }
    void BulletDestroy()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
