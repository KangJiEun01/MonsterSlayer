using UnityEngine;
public class YDSkyboxRotation : MonoBehaviour
{
    public float skyboxRotationSpeed = 1.0f;

    void Update()
    {
      // RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxRotationSpeed);
    }
}

