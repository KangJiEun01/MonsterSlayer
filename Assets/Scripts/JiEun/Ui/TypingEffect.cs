using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] Text tx;
    int maxNum = 0;
    string[] texts = {
        "인군 : 지금부터 필요한 조작법을 설명해 드리겠습니다.",
        "W/A/S/D 키로 이동할 수 있습니다.",
        "마우스 좌클릭으로 공격이 가능합니다.",
        "Shift 키를 누른 상태에서 이동을 하면 달려갑니다.",
        "Space 키를 누르면 점프합니다.",
        "E 키를 누르면 대쉬합니다.",
        "R 키를 누르면 재장전합니다.",
        "Z 키를 누르면 약초를 채집합니다.",
        "F 키를 누르면 회복 아이템을 사용합니다.",
        "I 키로 인벤토리 및 교환창을 열 수 있습니다.",
        "재료를 모아 무기를 교환해 잠금 해제 할 수 있습니다.",
        "O 키를 누르면 무기 교환창을 열 수있습니다.",
        "또 필요할 것이 있다면 저를 불러주세요. 무운을 빕니다."
    };

    void Start()
    {
        StartCoroutine(ShowTextWithTypingEffect());
    }
    private void Update()
    {
    }

    IEnumerator ShowTextWithTypingEffect()
    {
        for (int textIndex = 0; textIndex < texts.Length; textIndex++)
        {
            string currentText = texts[textIndex];
            yield return TypeText(currentText);
            yield return new WaitForSeconds(1f); // 다 타이핑 하고 1초
            // 글자 지움
            if(textIndex == texts.Length - 1)
            {
                background.SetActive(false);
            }
            yield return EraseText();
            // 다음 글자
            
        }
    }

    IEnumerator TypeText(string text)
    {
        float typingSpeed = 0.05f;
        for (int i = 0; i <= text.Length; i++)
        {
            tx.text = text.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator EraseText()
    {
        float erasingSpeed = 0.02f;
        for (int i = tx.text.Length; i >= 0; i--)
        {
            tx.text = tx.text.Substring(0, i);
            yield return new WaitForSeconds(erasingSpeed);
        }
    }
}