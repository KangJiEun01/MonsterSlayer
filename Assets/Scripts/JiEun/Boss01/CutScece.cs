using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScece : MonoBehaviour
{
    void Start()
    {
        Invoke("EndCutScece", 19.1f);
    }
    void EndCutScece()
    {
        SceneManager.LoadScene("YDBossStage02");
    }
}