using TMPro;
using UnityEngine;

public class ProgressValue : MonoBehaviour
{
    //[SerializeField]Text progress;
    [SerializeField] TextMeshProUGUI progress;


    // Use this for initialization
    void Start()
    {
        progress = GetComponent<TextMeshProUGUI>();

    }
    public void UpdateProgress(float content)
    {
        //float _progress = GetComponent<LoadingScene>().progress;
        //content = _progress;
        progress.text = Mathf.Round(content * 100) + "%";
    }
   // https://blog.naver.com/ksj12377/222455356062
}
