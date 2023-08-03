using UnityEngine;
using UnityEngine.AI;

//Moves agent using NavMesh.
public class EnemyNavmeshMover : AgentMover {  
  [SerializeField] private NavMeshAgent _agent;
  
  protected override void Awake() {
    base.Awake();
    if (_agent == null) {
      _agent = GetComponent<NavMeshAgent>();
    }
  }
  
  protected override void StateUpdate(bool isFiring) {
    if (!isFiring) {
      _aim.UpdateTarget();
      _agent.SetDestination(_aim.Target.position);
    } else {
      _agent.SetDestination(_agent.transform.position);
    }     
  }    
}
