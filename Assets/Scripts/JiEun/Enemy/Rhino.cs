using UnityEngine;
using UnityEngine.Pool;

public class Rhino : MonoBehaviour
{
    IObjectPool<Rhino> _pool;
    public void SetPool(IObjectPool<Rhino> pool)
    { _pool = pool; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
