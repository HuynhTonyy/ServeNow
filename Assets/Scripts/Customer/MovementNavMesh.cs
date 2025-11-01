using UnityEngine;
using UnityEngine.AI;
public class MovementNavMesh : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private void GoTo(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }
}