using UnityEngine;

public class Credit : MonoBehaviour
{
    Vector3 startPos = new Vector3(1200f, -500f, 0f);
    Vector3 ypos = new Vector3(0f, 1f, 0f);
    float _speed = 80f;
    private void OnEnable()
    {
        transform.position = startPos;
    }
    void Update()
    {
        transform.position += ypos * Time.deltaTime * _speed;
    }
}