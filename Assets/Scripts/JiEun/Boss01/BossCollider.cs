
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //PlayerHP -= 10;
            //Debug.Log("Hp--");
        }
    }
}
