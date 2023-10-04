using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControll : MonoBehaviour
{
    [SerializeField] GameObject manualUi;
    [SerializeField] GameObject fadeOut;
    private void Start()
    {
        manualUi.SetActive(false);
    }
    public void ClickStartButton()
    {
        SceneManager.LoadScene("LoadingScene");
        fadeOut.SetActive(true);
        fadeOut.GetComponent<FadeOut>().enabled = true;
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
