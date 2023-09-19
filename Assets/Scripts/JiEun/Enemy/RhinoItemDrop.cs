using UnityEngine;
using UnityEngine.Pool;

public class RhinoItemDrop : MonoBehaviour
{
    [SerializeField] GameObject _itemMeat;
    [SerializeField] GameObject _rhino;
    float _rhinohp = 100;


    IObjectPool<RhinoItemDrop> _pool;
    public void SetPool(IObjectPool<RhinoItemDrop> pool) { _pool = pool; }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            _rhinohp = 0;
        }
        if(_rhinohp <= 0)
        {
            Destroy(_rhino);
            //_pool.Release(this);
            Instantiate(_itemMeat);
        }
    }
}
