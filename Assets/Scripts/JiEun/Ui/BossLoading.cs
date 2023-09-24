using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BossLoading : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI loadingText; // �ε� % �ؽ�Ʈ
    [SerializeField] Slider loadingProgressBar; // �ε� ��
    //[SerializeField] TextMeshProUGUI progressT;
    public float progress;

    private void Start()
    {
        StartCoroutine(LoadAsyncScene());
        //progressT = GetComponent<TextMeshProUGUI>();
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("YDBossStage"); // �ε��� �� �̸����� ����

        // �ε� ���α׷��� �ٸ� ������Ʈ�ϰ� �ε� �ؽ�Ʈ�� ǥ��
        while (!asyncLoad.isDone)
        {
            progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // 0.9 = �� �ε� �Ϸᰪ.
            loadingProgressBar.value = progress;
            loadingText.text = (int)(progress * 100) + "%";
            //progressT.text = Mathf.Round(progress * 100) + "%";
            yield return null;
        }
    }
}
