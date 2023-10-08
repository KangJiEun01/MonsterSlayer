using UnityEngine;
using UnityEngine.SceneManagement;

public class stage3clear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GenericSingleton<GameManager>.Instance.SetCurrentStage(4);
            GenericSingleton<DataManager>.Instance.SaveData(0);
            SceneManager.LoadScene("BossStageLoadingScene");
        }
    }
}
