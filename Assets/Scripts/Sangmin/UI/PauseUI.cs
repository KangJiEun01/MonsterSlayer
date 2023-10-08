
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class PauseUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _quitCheckUI;
    [SerializeField] GameObject _optionUI;

    public void ShowOptionUI(bool isShow)
    {
        _optionUI.SetActive(isShow);
    }
    public void ShowQuitCheckUI(bool isShow)
    {
        _quitCheckUI.SetActive(isShow);
    }
    public void GoToMainMenu()
    {
        GenericSingleton<GameManager>.Instance.SetCurrentStage(0);
        GenericSingleton<BGMManager>.Instance.SetBgm(0);
        GenericSingleton<GameManager>.Instance.SetGameState(GameState.Loading);
        SceneManager.LoadSceneAsync("GameStart");
        
    }
    public void QuitCancle()
    {
        ShowQuitCheckUI(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
