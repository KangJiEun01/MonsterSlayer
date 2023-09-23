
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
            // 마우스 휠 값에 따라 스크롤 뷰의 값을 조절합니다.
            scrollRect.verticalNormalizedPosition += scrollDelta * scrollSpeed;
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition); // 값을 0과 1 사이로 제한합니다.
        }
    }
}