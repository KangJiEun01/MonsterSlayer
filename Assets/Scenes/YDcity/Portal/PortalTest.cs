using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTest : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ ������
    public Transform playerTransform; // �÷��̾��� Transform

    // Update is called once per frame
    void Update()
    {
        // 'j' Ű�� ������ �������� ����
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnPrefab();
        }
    }

    void SpawnPrefab()
    {
        // �÷��̾��� ���� ��ġ�� �������� z ������ 1 ���� �տ� ������ ����
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * 1.0f;
        Instantiate(prefabToSpawn, spawnPosition, playerTransform.rotation);
    }
}
