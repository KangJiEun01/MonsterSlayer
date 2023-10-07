using UnityEngine;
public class NeonCityEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnHp = 10f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    System.Collections.IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            GameObject inactiveEnemy = GetInactiveEnemy();
            if (inactiveEnemy != null)
            {
                inactiveEnemy.GetComponent<Target>().Hp = spawnHp;
                inactiveEnemy.SetActive(true);
            }
        }
    }
    GameObject GetInactiveEnemy()
    {
        foreach (var enemy in enemies)
        {
            if (!enemy.activeSelf)
            {
                return enemy;
            }
        }
        return null;
    }
}