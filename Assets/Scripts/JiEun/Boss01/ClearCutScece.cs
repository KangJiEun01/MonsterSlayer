using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearCutScece : MonoBehaviour
{
    void Start()
    {
        Invoke("EndCutScece", 30f);
    }
    void EndCutScece()
    {
        //PlayerUi.SetActive(true);
        SceneManager.LoadScene("GameStart");
        //GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(7.65f, 0f, 11f));
        //GenericSingleton<PlayerCon>.Instance.SetRotation(90);
    }
}
