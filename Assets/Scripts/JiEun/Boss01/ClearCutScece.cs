using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class ClearCutScece : MonoBehaviour
{
    void Start()
    {
        //Invoke("EndCutScece", 30f);
        Invoke("EndCutScece", 30f);
        GenericSingleton<GameManager>.Instance.SetGameState(GameState.Loading);

    }
    void EndCutScece()
    {
        //PlayerUi.SetActive(true);
        SceneManager.LoadScene("GameStart");
        GenericSingleton<GameManager>.Instance.SetCurrentStage(0);
        
        //GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(7.65f, 0f, 11f));
        //GenericSingleton<PlayerCon>.Instance.SetRotation(90);
    }
}
    