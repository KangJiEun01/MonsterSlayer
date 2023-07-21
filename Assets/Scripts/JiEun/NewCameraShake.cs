using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraShake : MonoBehaviour
{
    //[SerializeField] GameObject pos;
    //public float shakeDuration = 0.2f;  // ��鸲 ���� �ð�
    public float shakeAmount = 0.3f;    // ��鸲 ����
    public float decreaseFactor = 3f;   // ��鸲 ���� ����
    private Vector3 originalPosition;
    // private float currentShakeDuration = 1f;

    float currentShakeDuration = 2f;

    void Start()
    {
        //originalPosition = transform.position;
        //originalPosition= pos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentShakeDuration > 0)
        {
            Shake();
        }
        else
         {
            // ��鸲 ����
            transform.localPosition = originalPosition;
            currentShakeDuration = 0f;
        }
    }
    void Shake()
    {
        transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;  // ī�޶� ��ġ ����
        currentShakeDuration -= Time.deltaTime * decreaseFactor; //�ð�����
    }
}
