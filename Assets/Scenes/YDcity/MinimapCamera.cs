using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform target;
    public float offsetRatio;

    Camera cam;
    Vector2 size;
    public Transform indicator;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // "Player" 태그를 가진 오브젝트를 찾아서 플레이어로 설정
        cam = GetComponent<Camera>();
        size = new Vector2(cam.orthographicSize, cam.orthographicSize * cam.aspect);
    }

    void Update()
    {
        Vector3 targetForwardVector = target.forward;
        targetForwardVector.y = 0;
        targetForwardVector.Normalize();

        Vector3 position = new Vector3(target.transform.position.x, 1, target.transform.position.z)
                           + targetForwardVector * offsetRatio * cam.orthographicSize;
        transform.position = position;
        transform.eulerAngles = new Vector3(90, 0, -target.eulerAngles.y);
    }

    public void ShowBorderIndicator(Vector3 position)
    {
        float reciprocal;
        float rotation;
        Vector2 distance = new Vector3(transform.position.x - position.x, transform.position.z - position.z);

        distance = Quaternion.Euler(0, 0, target.eulerAngles.y) * distance;

        if(Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            reciprocal = Mathf.Abs(size.x / distance.x);
            rotation = (distance.x > 0) ? 90 : -90;
        }
        else
        {
            reciprocal = Mathf.Abs(size.y / distance.y);
            rotation = (distance.y > 0) ? 180 : 0;
        }

        indicator.localPosition = new Vector3(distance.x * -reciprocal, distance.y * -reciprocal, 1);
        indicator.localEulerAngles = new Vector3(0, 0, rotation);
    }

    public void HideBorderIncitator()
    {
        indicator.gameObject.SetActive(false);
    }
}