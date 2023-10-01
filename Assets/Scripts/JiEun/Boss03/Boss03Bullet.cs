using UnityEngine;

public class Boss03Bullet : MonoBehaviour
{
    Vector3 _dir;
    Vector3 _player;
    float _speed;

    void Update()
    {
        transform.position += _dir * Time.deltaTime * _speed;

        Vector3 targetDirection = _dir - transform.position;
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * _speed);
        }

        // 총알을 목표 방향으로 이동시키기
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
    public void Init(Vector3 dir, float speed)
    {
        Debug.Log(dir);
        _dir = dir;
        _speed = speed;
        Invoke("DeleteBullet", 5f);
    }
    void DeleteBullet()
    {
        Destroy(gameObject);
    }
}
