using UnityEngine;
public class NeonCityEnemyController : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] float spawnHp = 10f;

    void Update()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].activeSelf)
            {
                StartCoroutine(RespawnEnemy(i)); //�׾��ٸ� 7�� �� ������
            }
        }
    }
    System.Collections.IEnumerator RespawnEnemy(int index)
    {
        yield return new WaitForSeconds(7f); // 7��

        GameObject inactiveEnemy = enemies[index];
        inactiveEnemy.GetComponent<Target>().Hp = spawnHp;
        inactiveEnemy.SetActive(true);
    }
}