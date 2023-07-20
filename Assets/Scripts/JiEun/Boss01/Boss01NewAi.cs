
using System.Collections;
using UnityEngine;

public class Boss01NewAi : MonoBehaviour
{
    Animator animator;

    public bool _attack = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("In");
        //StartCoroutine(BossAttackRoutine());
    }
    private IEnumerator BossAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            StartAttack();

            yield return new WaitForSeconds(5f);
        }
    }

    private void StartAttack()
    {
        if(_attack ==false)
        {
            _attack = true;




            _attack=false;
        }
    }

    void Update()
    {
        
    }
}
