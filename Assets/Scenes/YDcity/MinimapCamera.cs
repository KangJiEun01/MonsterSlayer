using UnityEngine;

public class MinimapCamera : GenericSingleton<MinimapCamera>
{
    public Transform target;
    public float offsetRatio;

    Camera cam;
    Vector2 size;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("MiniMapFind").transform;
        cam = GetComponent<Camera>();
        size = new Vector2(cam.orthographicSize, cam.orthographicSize * cam.aspect);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 position = target.position;
            position.y = transform.position.y; 

            transform.LookAt(position + Vector3.up, Vector3.up);

            Vector3 targetForwardVector = target.forward;
            targetForwardVector.y = 0;
            targetForwardVector.Normalize();
            Vector3 cameraPosition = position + targetForwardVector * offsetRatio * cam.orthographicSize;
            transform.position = cameraPosition;

            transform.rotation = Quaternion.Euler(90, 0, -target.eulerAngles.y);
        }
    }

}
