using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class NeonLoading : MonoBehaviour
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
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("YDUnderCity"); // �ε��� �� �̸����� ����
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("YDNeonCity");

        // �ε� ���α׷��� �ٸ� ������Ʈ�ϰ� �ε� �ؽ�Ʈ�� ǥ��
        asyncLoad.allowSceneActivation = false;
        // while (!asyncLoad.isDone)
        while (asyncLoad.progress < 0.9f)
        {
            progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // 0.9 = �� �ε� �Ϸᰪ.
            loadingProgressBar.value = progress;
            loadingText.text = (int)(progress * 100) + "%";
            //progressT.text = Mathf.Round(progress * 100) + "%";
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        //GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(14.8f, 3f, 50.59f)); //�����Ƽ
        GenericSingleton<ParentSingleTon>.Instance.SetPosition(new Vector3(0f, 0f, 0f));
        GenericSingleton<ParentSingleTon>.Instance.SetRotation(0);
        GenericSingleton<PlayerCon>.Instance.SetPosition(new Vector3(-11.47f, 7f, 2.26f)); //���׿½�Ƽ
        GenericSingleton<PlayerCon>.Instance.SetRotation(90);
        
    }
}
