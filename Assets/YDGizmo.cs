using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YDGizmo : MonoBehaviour
{
    public enum Type { NORMAL, WAYPOINT}
    private const string wayAPointFile = "Enemy";
    public Type type = Type.NORMAL;

    public Color _color = Color.yellow;
    public float _radius = .1f;

    private void OnDrawGizmos()
    {
        if(type== Type.NORMAL)
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _radius);
        }
        else
        {
            Gizmos.color = _color;
            Gizmos.DrawIcon(transform.position + Vector3.up * 1f, wayAPointFile, true);
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        Gizmos.color = _color;

        Gizmos.DrawSphere(this.transform.position, _radius);
    }
}
