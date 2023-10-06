using UnityEngine;
using UnityEngine.SceneManagement;

public class stage2clear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GenericSingleton<DataManager>.Instance.SaveData(0);
            SceneManager.LoadScene("UnderCityLoadingScene");
        }
    }
}
