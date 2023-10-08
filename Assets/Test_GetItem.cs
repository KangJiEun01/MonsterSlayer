using UnityEngine.SceneManagement;
using UnityEngine;

public class Test_GetItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            GenericSingleton<WeaponManager>.Instance.AllUnlock();
        }
    }
}
