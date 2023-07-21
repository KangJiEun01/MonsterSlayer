using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //public float shakeDuration = 0.2f;  // 흔들림 지속 시간
    public float shakeAmount = 0.3f;    // 흔들림 정도
    public float decreaseFactor = 3f;   // 흔들림 감소 비율

    private Vector3 originalPosition;
    // private float currentShakeDuration = 1f;
    //GameObject boss;
    [SerializeField] GameObject boss;
    float currentShakeDuration=2f;


    void Start()
    {
       // boss = GameObject.FindGameObjectWithTag("Boss");
        //originalPosition = transform.localPosition;
        originalPosition = transform.position;
    }
    private void OnEnable()
    {
        currentShakeDuration = 2f;
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
            // currentShakeDuration = 0f;
            transform.localPosition = originalPosition;
            ShakeDuration();
            //Invoke("ShakeDuration", 1f);
            //GetComponent<CameraShake>().enabled = false;
         }
    }
    void Shake()
    {
        transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;  // 카메라 위치 랜덤
        currentShakeDuration -= Time.deltaTime * decreaseFactor; //시간감소
    }
    //void Shake(float currentShakeDuration)
    //{
    //    if (currentShakeDuration > 0)
    //    {
    //        // 카메라 위치 랜덤
    //        transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

    //        // 시간감소
    //        currentShakeDuration -= Time.deltaTime * decreaseFactor;
    //    }
    //    else
    //    {
    //        // 흔들림 종료
    //        // currentShakeDuration = 0f;
    //        transform.localPosition = originalPosition;
    //        ShakeDuration();
    //        //Invoke("ShakeDuration", 1f);
    //        //GetComponent<CameraShake>().enabled = false;
    //    }
    //}
    void ShakeDuration()
    {
        GetComponent<CameraShake>().enabled = false;
        //currentShakeDuration = 1f;
    }
}
