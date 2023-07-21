using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02NewAi : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        //animator.Play("In");
        //animator.Play("2_Atk1"); 
        //animator.Play("2_Atk2");
        animator.Play("2_Hit");
        //animator.Play("2_Run"); 
    }
    void Update()
    {
        
    }
}
