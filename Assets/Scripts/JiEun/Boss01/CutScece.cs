using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScece : MonoBehaviour
{
    //[SerializeField] GameObject PlayerUi;
    void Start()
    {
        //PlayerUi.SetActive(false);
        Invoke("EndCutScece", 19.1f);
    }
    void EndCutScece()
    {
        //PlayerUi.SetActive(true);
        SceneManager.LoadScene("YDBossStage02");
        GenericSingleton<ParentSingleTon>.Instance.SetPosition(new Vector3(0, 0, 0));
        GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(10f, 2f, 18f));
        GenericSingleton<PlayerCon>.Instance.SetRotation(90);
    }
}