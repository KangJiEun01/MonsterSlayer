using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BossLoading : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI loadingText; // 로딩 % 텍스트
    [SerializeField] Slider loadingProgressBar; // 로딩 바
    //[SerializeField] TextMeshProUGUI progressT;
    public float progress;

    private void Start()
    {
        StartCoroutine(LoadAsyncScene());
        //progressT = GetComponent<TextMeshProUGUI>();
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("YDBossStage"); // 로드할 씬 이름으로 변경

        // 로딩 프로그레스 바를 업데이트하고 로딩 텍스트를 표시
        while (!asyncLoad.isDone)
        {
            progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // 0.9 = 씬 로딩 완료값.
            loadingProgressBar.value = progress;
            loadingText.text = (int)(progress * 100) + "%";
            //progressT.text = Mathf.Round(progress * 100) + "%";
            yield return null;
        }
    }
}
