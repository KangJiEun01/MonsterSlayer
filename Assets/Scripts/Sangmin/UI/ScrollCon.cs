
using UnityEngine;
using UnityEngine.UI;

public class ScrollCon : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] float scrollSpeed = 0.1f;

    void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        if (scrollDelta != 0)
        {
            // ���콺 �� ���� ���� ��ũ�� ���� ���� �����մϴ�.
            scrollRect.verticalNormalizedPosition += scrollDelta * scrollSpeed;
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition); // ���� 0�� 1 ���̷� �����մϴ�.
        }
    }
}