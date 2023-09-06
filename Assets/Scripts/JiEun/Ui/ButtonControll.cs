using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControll : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ClickStartButton()
    {
        SceneManager.LoadScene("MapSelect");
    }
    public void ClickbackButton() 
    {
        SceneManager.LoadScene("GameStart");
    }
    public void ClickLevelMapButton()
    {
        SceneManager.LoadScene("YDNeonCity");
    }
}
