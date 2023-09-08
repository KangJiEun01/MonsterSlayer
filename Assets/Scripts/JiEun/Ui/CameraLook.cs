using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] GameObject camera;
    void Update()
    {
        transform.LookAt(camera.transform);
    }
}
