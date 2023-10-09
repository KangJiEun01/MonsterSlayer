using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] Text tx;
    int maxNum = 0;
    string[] texts = {
        "�α� : ���ݺ��� �ʿ��� ���۹��� ������ �帮�ڽ��ϴ�.",
        "W/A/S/D Ű�� �̵��� �� �ֽ��ϴ�.",
        "���콺 ��Ŭ������ ������ �����մϴ�.",
        "Shift Ű�� ���� ���¿��� �̵��� �ϸ� �޷����ϴ�.",
        "Space Ű�� ������ �����մϴ�.",
        "E Ű�� ������ �뽬�մϴ�.",
        "R Ű�� ������ �������մϴ�.",
        "Z Ű�� ������ ���ʸ� ä���մϴ�.",
        "F Ű�� ������ ȸ�� �������� ����մϴ�.",
        "I Ű�� �κ��丮 �� ��ȯâ�� �� �� �ֽ��ϴ�.",
        "��Ḧ ��� ���⸦ ��ȯ�� ��� ���� �� �� �ֽ��ϴ�.",
        "O Ű�� ������ ���� ��ȯâ�� �� ���ֽ��ϴ�.",
        "�� �ʿ��� ���� �ִٸ� ���� �ҷ��ּ���. ������ ���ϴ�."
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
            yield return new WaitForSeconds(1f); // �� Ÿ���� �ϰ� 1��
            // ���� ����
            if(textIndex == texts.Length - 1)
            {
                background.SetActive(false);
            }
            yield return EraseText();
            // ���� ����
            
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