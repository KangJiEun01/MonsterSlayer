using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControll : MonoBehaviour
{
    [SerializeField] GameObject manualUi;
    private void Start()
    {
        manualUi.SetActive(false);
    }
    public void ClickStartButton()
    {
        GenericSingleton<UIBase>.Instance.ShowLodingSceneUI(true);
        SceneManager.LoadScene("LoadingScene");
        // Debug.Log("게임스타트");
        //SceneManager.LoadScene("MapSelect");
    }
    public void ClickbackButton() 
    {
        SceneManager.LoadScene("GameStart");
    }
    public void ClickLevelMapButton()
    {
        SceneManager.LoadScene("LoadingScene");
    }
    public void ClickDemeVersion()
    {
        SceneManager.LoadScene("BossStageLoadingScene");
    }
    public void ClickManual()
    {
        manualUi.SetActive(true);
    }
    public void ManualActive()
    {
        manualUi.SetActive(false);
    }
}
