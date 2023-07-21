using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //public float shakeDuration = 0.2f;  // ��鸲 ���� �ð�
    public float shakeAmount = 0.3f;    // ��鸲 ����
    public float decreaseFactor = 3f;   // ��鸲 ���� ����

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
            // ��鸲 ����
            // currentShakeDuration = 0f;
            transform.localPosition = originalPosition;
            ShakeDuration();
            //Invoke("ShakeDuration", 1f);
            //GetComponent<CameraShake>().enabled = false;
         }
    }
    void Shake()
    {
        transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;  // ī�޶� ��ġ ����
        currentShakeDuration -= Time.deltaTime * decreaseFactor; //�ð�����
    }
    //void Shake(float currentShakeDuration)
    //{
    //    if (currentShakeDuration > 0)
    //    {
    //        // ī�޶� ��ġ ����
    //        transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

    //        // �ð�����
    //        currentShakeDuration -= Time.deltaTime * decreaseFactor;
    //    }
    //    else
    //    {
    //        // ��鸲 ����
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
