using UnityEngine.SceneManagement;
using UnityEngine;

public class Test_GetItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            GenericSingleton<ItemSaver>.Instance.DemoSceneItem();
            GenericSingleton<ExchangeSystem>.Instance.Init();
            GenericSingleton<PlayerCon>.Instance.Init();
            GenericSingleton<UIBase>.Instance.Init();
        }
    }
}
