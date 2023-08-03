using UnityEngine;

//Astract parent for movers
[RequireComponent(typeof(IBehaviourController))]
public abstract class AgentMover : MonoBehaviour {
  [SerializeField] protected AimController _aim;

  protected IBehaviourController _stateController;

  protected virtual void Awake() {
    if (_stateController == null) {
      _stateController = GetComponent<IBehaviourController>();
    }
    if (_aim == null) {
      _aim = GetComponent<AimController>();
    }
  }

  protected virtual void OnEnable() {
    _stateController.OnStatusChange += StateUpdate;
  } 

  protected virtual void OnDisable() {
    _stateController.OnStatusChange -= StateUpdate;
  }

  protected abstract void StateUpdate(bool canFire);
}
