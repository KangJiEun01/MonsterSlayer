using UnityEngine;

public class NewCameraShake : MonoBehaviour
{
    //[SerializeField] GameObject pos;
    //public float shakeDuration = 0.2f;  // 흔들림 지속 시간
    [SerializeField] float shakeAmount = 0.2f;    // 흔들림 정도
    [SerializeField] float decreaseFactor = 3f;   // 흔들림 감소 비율
    private Vector3 originalPosition;
    // private float currentShakeDuration = 1f;

    float currentShakeDuration = 2f;

    void Start()
    {
        //originalPosition = transform.position;
        //originalPosition= pos.transform.position;
    }

    // Update is called once per frame
    public void OnEnable()
    {
        currentShakeDuration = 2f;
        Debug.Log("카메라켜짐");
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            Shake();
        }
        else
         {
            // 흔들림 종료
            transform.localPosition = originalPosition;
            currentShakeDuration = 0f;
            GetComponent<NewCameraShake>().enabled = false;
        }
    }
    public void Shake()
    {
        transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;  // 카메라 위치 랜덤
        currentShakeDuration -= Time.deltaTime * decreaseFactor; //시간감소
    }
}
