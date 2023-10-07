using UnityEngine;
using UnityEngine.SceneManagement;

public class stage2clear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GenericSingleton<GameManager>.Instance.SetCurrentStage(3);
            GenericSingleton<DataManager>.Instance.SaveData(0);
            SceneManager.LoadScene("UnderCityLoadingScene");
        }
    }
}
