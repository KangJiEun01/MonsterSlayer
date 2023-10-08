using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("EnemySpawn", 7f);
    }
    void EnemySpawn()
    {
        gameObject.SetActive(true);
        GetComponent<Enemy01controller>().enabled = true;
    }
}
