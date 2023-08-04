using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Boss02Dead : MonoBehaviour
{
    [SerializeField] GameObject Boss03;

    private void OnEnable()
    {
        Boss03.transform.position = transform.position;
        Invoke("Boss2Dead", 0.5f);
    }
    void Boss2Dead()
    {
        Destroy(gameObject);
        Boss03.GetComponent<Boss03NewAi>().enabled = true;
        Boss03.SetActive(true);
    }
    void Start()
    {
        
    }
    void Update()
    {

    }
}
