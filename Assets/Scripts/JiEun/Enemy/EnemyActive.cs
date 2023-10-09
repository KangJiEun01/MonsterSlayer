using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("EnemySpawn", 14f);
    }
    void EnemySpawn()
    {
        gameObject.SetActive(true);
        GetComponent<Enemy01controller>().enabled = true;
    }
}
