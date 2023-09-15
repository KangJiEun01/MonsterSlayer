using UnityEngine;
using UnityEngine.Pool;
public class RhinoController : MonoBehaviour
{
    [SerializeField] GameObject _RhinoItem;

    float _rhinoHp = 100;
    IObjectPool<RhinoController> _pool;
    public void SetPool(IObjectPool<RhinoController> pool)
    { _pool = pool; }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            _rhinoHp = 0;
        }
        if (_rhinoHp <= 0)
        {
            _pool.Release(this);
            Instantiate(_RhinoItem);
        }
    }
    private void FixedUpdate()
    {
    }
}
