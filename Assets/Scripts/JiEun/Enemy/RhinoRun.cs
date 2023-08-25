using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class RhinoRun : MonoBehaviour
{
    [SerializeField] GameObject player;

    Animator rhinoAni;
    float moveSpeed = 2.0f;
    Vector3 targetPosition;

    private void Awake()
    {
        rhinoAni = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        rhinoAni.Play("Run");
    }
    void Update()
    {
        targetPosition = new Vector3(player.transform.position.x, 0 , player.transform.position.z);
        transform.LookAt(targetPosition);
        if (Vector3.Distance(transform.position, player.transform.position) < 10f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, player.transform.position) < 3f)
            {
                GetComponent<RhinoRun>().enabled = false;
                GetComponent<RhinoAttack>().enabled = true;
            }
        }
          if (Vector3.Distance(transform.position, player.transform.position) > 10f)
        {
            RhinoIdle();
        }
    }
    void RhinoIdle()
    {
        GetComponent<RhinoRun>().enabled = false;
        GetComponent<RhinoAi>().enabled = true;
    }
}
