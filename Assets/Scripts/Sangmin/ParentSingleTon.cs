
using UnityEngine;

public class ParentSingleTon : GenericSingleton<ParentSingleTon>
{
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public void SetRotation(float rotY)
    {
        transform.localEulerAngles = new Vector3(0, rotY, 0);
    }
}
