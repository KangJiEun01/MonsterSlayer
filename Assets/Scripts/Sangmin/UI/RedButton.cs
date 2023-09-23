
using UnityEngine;

public class RedButton : MonoBehaviour
{
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("NonClick");
    }
}
