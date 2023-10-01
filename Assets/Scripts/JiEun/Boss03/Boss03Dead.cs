using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss03Dead : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("Boss02Scece");
    }
}
