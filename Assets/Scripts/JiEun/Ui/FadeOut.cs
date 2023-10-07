using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField] GameObject fadeobj;
    Image fadeImage;
    float fadeDuration = 4.0f; //페이드 아웃 시간
    float currentTime = 0f;
    void Start()
    {
        //fadeImage=GetComponent<Image>();
    }
    private void OnEnable()
    {
        fadeImage = GetComponent<Image>();
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
        Invoke("ActiveFalse", 5f);
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        float alpha = 1 - Mathf.Clamp01(currentTime / fadeDuration); // 0->1을 1->0으로 변경
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);

            //fadeImage.CrossFadeAlpha(1f, fadeDuration, false);
            //fadeobj.GetComponent<FadeOut>().enabled = false;
            //fadeobj.SetActive(false);
    }
    void ActiveFalse()
    {
        fadeobj.GetComponent<FadeOut>().enabled = false;
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
        currentTime = 0f;
        fadeobj.SetActive(false);
    }
}
