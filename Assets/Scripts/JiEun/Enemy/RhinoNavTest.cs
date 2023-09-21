using UnityEngine;
using UnityEngine.AI;

public class RhinoNavTest : MonoBehaviour
{
    [SerializeField] GameObject _target;
    NavMeshAgent _agent;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_target.transform.position);
    }
    void Update()
    {
        
    }
}
