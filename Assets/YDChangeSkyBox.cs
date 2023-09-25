using UnityEngine;

public class YDChangeSkyBox : MonoBehaviour
{
    public Material[] skyboxMaterials; // Skybox Material �迭
    private int currentMaterialIndex = 0;
    private Camera mainCamera;

    private float timer = 0f;
    public float changeInterval = 30f; // 30�ʸ��� Skybox ����

    void Start()
    {
        mainCamera = Camera.main;

        // �ʱ� Skybox Material ����
        if (skyboxMaterials.Length > 0)
        {
            RenderSettings.skybox = skyboxMaterials[currentMaterialIndex];
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // ���� �������� Skybox ����
        if (timer >= changeInterval && skyboxMaterials.Length > 0)
        {
            currentMaterialIndex = (currentMaterialIndex + 1) % skyboxMaterials.Length;
            RenderSettings.skybox = skyboxMaterials[currentMaterialIndex];
            timer = 0f;
        }
    }
}