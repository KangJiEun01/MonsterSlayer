using UnityEngine;
using UnityEngine.SceneManagement;

public class stage2clear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("UnderCityLoadingScene");
        }
    }
}
