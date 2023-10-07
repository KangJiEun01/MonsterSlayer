using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControll : MonoBehaviour
{
    [SerializeField] GameObject manualUi;
    [SerializeField] GameObject _loadButton;
    private void Start()
    {
        manualUi.SetActive(false);

        if (GenericSingleton<DataManager>.Instance.Data() != null)
        {
            _loadButton.SetActive(true);
        }
        else
        {
            _loadButton.SetActive(false);
        }
    }
    public void ClickStartButton()
    {
        GenericSingleton<GameManager>.Instance.StartNewGame();
        SceneManager.LoadScene("LoadingScene");
    }
    public void ClickLoadButton()
    {
        GenericSingleton<UIBase>.Instance.ShowLodingSceneUI(true);
        int currentStage = GenericSingleton<GameManager>.Instance.LoadGame();
        switch (currentStage)
        {
            case 1:
                SceneManager.LoadScene("LoadingScene");
                break;
            case 2:
                SceneManager.LoadScene("NeonCityLoadingScene");
                break;
            case 3:
                SceneManager.LoadScene("UnderCityLoadingScene");
                break;
            case 4:
                SceneManager.LoadScene("BossStageLoadingScene");
                break;
        }
        
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
        GenericSingleton<UIBase>.Instance.ShowLodingSceneUI(true);
        GenericSingleton<GameManager>.Instance.DemonScene();
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
