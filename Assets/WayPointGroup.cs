using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class WayPointGroup : MonoBehaviour
{
    public static WayPointGroup instance = null;
    public List<Transform> wayPoints;

    private void Awake()
    {
        if(instance == null)
            instance= this;

        Init();
    }

    private void Init()
    {
        GetComponentsInChildren<Transform>(wayPoints);
        wayPoints.Remove(this.transform);
    }

    public bool IsValid(int idx)
    {
        return (idx >= 0 && idx < wayPoints.Count);
    }

    public Vector3 GetPoint(int idx)
    {
        Assert.IsTrue(IsValid(idx));
        return wayPoints[idx].position;
    }
}


