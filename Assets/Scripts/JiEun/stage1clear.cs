using UnityEngine;
using UnityEngine.SceneManagement;

public class stage1clear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            GenericSingleton<GameManager>.Instance.SetCurrentStage(2);
            GenericSingleton<DataManager>.Instance.SaveData(0);
            SceneManager.LoadScene("NeonCityLoadingScene");
        }
    }
}
