using UnityEngine;

public class StartStage : MonoBehaviour
{
    [SerializeField] GameObject Fadeout;
    void Start()
    {
        Fadeout.SetActive(true);
        Fadeout.GetComponent<FadeOut>().enabled = true;
    }
}