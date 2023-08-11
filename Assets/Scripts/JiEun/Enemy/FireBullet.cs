using UnityEngine;

public class FireBullet : MonoBehaviour
{
    Vector3 _dir;
    float _speed;
    void Start()
    {
        
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DeleteBullet();
    }
}