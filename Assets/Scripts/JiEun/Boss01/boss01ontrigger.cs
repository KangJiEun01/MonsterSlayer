using UnityEngine;
using UnityEngine.SceneManagement;

public class boss01ontrigger : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player"))
        {
            //bossPrefab.SetActive(true);
            SceneManager.LoadScene("Boss01Scece 1");
            //Debug.Log("�ƾ��̵�");
        } 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //bossPrefab.SetActive(true);
            SceneManager.LoadScene("Boss01Scece 1");
            //Debug.Log("�ƾ��̵�");
        }
    }
}
