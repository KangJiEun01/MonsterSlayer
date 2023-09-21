using UnityEngine;
using UnityEngine.SceneManagement;

public class stage1clear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("NeonCityLoadingScene");
        }
    }
}
