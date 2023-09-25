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
        GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(7.65f, 0f, 11f));
        GenericSingleton<PlayerCon>.Instance.SetRotation(90);
    }
}