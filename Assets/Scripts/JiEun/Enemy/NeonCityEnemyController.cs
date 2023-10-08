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
                StartCoroutine(RespawnEnemy(i)); //죽었다면 7초 뒤 리스폰
            }
        }
    }
    System.Collections.IEnumerator RespawnEnemy(int index)
    {
        yield return new WaitForSeconds(7f); // 7초

        GameObject inactiveEnemy = enemies[index];
        inactiveEnemy.GetComponent<Target>().Hp = spawnHp;
        inactiveEnemy.SetActive(true);
    }
}