using UnityEngine;

public class YDChangeSkyBox : MonoBehaviour
{
    public Material[] skyboxMaterials; // Skybox Material 배열
    private int currentMaterialIndex = 0;
    private Camera mainCamera;

    private float timer = 0f;
    public float changeInterval = 30f; // 30초마다 Skybox 변경

    void Start()
    {
        mainCamera = Camera.main;

        // 초기 Skybox Material 설정
        if (skyboxMaterials.Length > 0)
        {
            RenderSettings.skybox = skyboxMaterials[currentMaterialIndex];
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 일정 간격으로 Skybox 변경
        if (timer >= changeInterval && skyboxMaterials.Length > 0)
        {
            currentMaterialIndex = (currentMaterialIndex + 1) % skyboxMaterials.Length;
            RenderSettings.skybox = skyboxMaterials[currentMaterialIndex];
            timer = 0f;
        }
    }
}