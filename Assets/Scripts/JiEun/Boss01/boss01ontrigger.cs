using UnityEngine;
using UnityEngine.SceneManagement;

public class boss01ontrigger : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //bossPrefab.SetActive(true);
            SceneManager.LoadScene("Boss01Scece 1");
            //Debug.Log("ÄÆ¾ÀÀÌµ¿");
        } 
    }
}
