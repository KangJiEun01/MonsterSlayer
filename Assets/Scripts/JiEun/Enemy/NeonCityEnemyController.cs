using UnityEngine;
public class NeonCityEnemyController : MonoBehaviour
{
    [SerializeField] GameObject[] Enemys;
    [SerializeField] GameObject[] EnemyPos;

    bool[] EnemysSpawn;
    int num = 0;
    float spawnHp = 10;

    private void Start()
    {
        EnemysSpawn = new bool[Enemys.Length];
        for(int i = 0; i < Enemys.Length; i++)
        {
            EnemysSpawn[i] = true;
        }
    }
    void Update()
    {
        for (int i = 0; i < Enemys.Length; i++)
        {
            if (!Enemys[i].activeSelf)
            {
                num = i;
                Invoke("Spawn", 5f);
            }
        }
    }
    void Spawn()
    {
        Enemys[num].GetComponent<Target>().Hp= spawnHp;
        Enemys[num].SetActive(true);
        EnemysSpawn[num] = true;
        EnemysSpawn[num] = true;
    }
}
