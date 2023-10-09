using UnityEngine;

public class Boss02Dead : MonoBehaviour
{
    [SerializeField] GameObject Boss03;

    private void OnEnable()
    {
        GetComponent<Animator>().Play("Idle");
        Boss03.transform.position = transform.position;
        Invoke("Boss2Dead", 2f);
        //Invoke("Boss3On", 4f);
    }
    void Boss2Dead()
    {
        Boss03.GetComponent<Boss03LastAi>().enabled = true;
        Boss03.SetActive(true);
        Destroy(gameObject);
    }
    void Boss3On()
    {
        Destroy(gameObject);
    }
}
