using UnityEngine;
using UnityEngine.Pool;
public class RhinoController : MonoBehaviour
{
    [SerializeField] GameObject _rhino;
    //[SerializeField] GameObject _RhinoItem;

    float _rhinoHp = 100;
    int _rhinoCount = 30;

    IObjectPool<RhinoItemDrop> _pool;
    //public void SetPool(IObjectPool<RhinoController> pool){ _pool = pool; }
    void Awake()
    {
        _pool = new ObjectPool<RhinoItemDrop>(CreatRhino, OnGetRhino, OnReleaseRhino, OnDestroyRhino, maxSize: _rhinoCount);
    }
    RhinoItemDrop CreatRhino()
    {
        RhinoItemDrop temp = Instantiate(_rhino).GetComponent<RhinoItemDrop>();
       // temp.SetPool(_pool);
        return temp;
    }
    void OnGetRhino(RhinoItemDrop rhi)
    {
        rhi.gameObject.SetActive(true);
    }
    void OnReleaseRhino(RhinoItemDrop rhi)
    {
        rhi.gameObject.SetActive(false);
    }
    void OnDestroyRhino(RhinoItemDrop rhi)
    {
        Destroy(rhi.gameObject);
    }
}
