using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTest : MonoBehaviour
{
    public GameObject prefabToSpawn; // 생성할 프리팹
    public Transform playerTransform; // 플레이어의 Transform

    // Update is called once per frame
    void Update()
    {
        // 'j' 키를 누르면 프리팹을 생성
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnPrefab();
        }
    }

    void SpawnPrefab()
    {
        // 플레이어의 현재 위치를 기준으로 z 축으로 1 단위 앞에 프리팹 생성
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * 1.0f;
        Instantiate(prefabToSpawn, spawnPosition, playerTransform.rotation);
    }
}
