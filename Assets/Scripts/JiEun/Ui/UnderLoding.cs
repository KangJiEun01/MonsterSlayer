using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UnderLoding : MonoBehaviour
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("YDUnderCity"); // 로드할 씬 이름으로 변경
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("YDNeonCity");

        // 로딩 프로그레스 바를 업데이트하고 로딩 텍스트를 표시
        asyncLoad.allowSceneActivation = false;
        // while (!asyncLoad.isDone)
        while (asyncLoad.progress < 0.9f)
        {
            progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // 0.9 = 씬 로딩 완료값.
            loadingProgressBar.value = progress;
            loadingText.text = (int)(progress * 100) + "%";
            //progressT.text = Mathf.Round(progress * 100) + "%";
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        //GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(14.8f, 3f, 50.59f)); //구언더시티
        //GenericSingleton<ParentSingleTon>.Instance.SetPosition(new Vector3(-14.8f, 20f, -112.5f)); //언더시티
        //GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(-15.47f, 5f, 2.26f)); //네온시티
        //GenericSingleton<ParentSingleTon>.Instance.SetRotation(180);
        GenericSingleton<ParentSingleTon>.Instance.SetPosition(new Vector3(0f, 0f, 0f));
        GenericSingleton<ParentSingleTon>.Instance.SetRotation(0);
        GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(14.3f, 3.5f, 46.8f)); //구네온시티
        GenericSingleton<PlayerCon>.Instance.SetRotation(-180);
    }
}
