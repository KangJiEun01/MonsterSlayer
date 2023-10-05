
using UnityEngine;

public class bulletHole : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DestroyBullet", 2f);
    }
    void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}
