using UnityEngine;

public class NeonCityEnemyController : MonoBehaviour
{
    [SerializeField] GameObject[] Enemys;
    [SerializeField] GameObject[] EnemyPos;
    [SerializeField] GameObject[] DropItem;

    bool[] itemSpawn;

    private void Start()
    {
        itemSpawn = new bool[Enemys.Length];
        for(int i = 0; i < Enemys.Length; i++)
        {
            itemSpawn[i] = false;
        }
    }
    void Update()
    {
        for (int i = 0; i < Enemys.Length; i++)
        {
            if (!Enemys[i].activeSelf && !itemSpawn[i])
            {
                int num = Random.Range(0, 4);
                Transform ItemDrop = EnemyPos[i].transform;
                Instantiate(DropItem[num]);
                DropItem[num].transform.position = ItemDrop.transform.position;
                Debug.Log(ItemDrop + "»ý¼º");
               // itemSpawn[i] = true;
            }
        }
    }
}
