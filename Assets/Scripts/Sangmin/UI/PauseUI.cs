using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //메인씬 가는함수
    }
    public void QuitCancle()
    {
        ShowQuitCheckUI(false);
    }
    public void QuitGame()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }
}
