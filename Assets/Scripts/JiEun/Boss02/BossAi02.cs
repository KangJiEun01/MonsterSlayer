using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi02 : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("In");
    }
    void Update()
    {
        
    }
}
