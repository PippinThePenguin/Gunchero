using UnityEngine;

//Mover for flying enemy. Moves Body to Target direction nonphysicly.
public class FlyEnemyMover : AgentMover {
  [SerializeField] protected Transform _body;
  [SerializeField] protected float _moveSpeed = 2f;

  protected bool _isMoving = false; 

  protected void Update() {
    Moving();
  }

  protected void Moving() {
    if(!_isMoving) {
      return;
    }

    _aim.UpdateTarget();
    _body.rotation = Quaternion.LookRotation(_aim.Target.position - _body.position);
    _body.position += transform.forward * _moveSpeed * Time.deltaTime;
  }

  protected override void StateUpdate(bool canFire) {
    _isMoving = !canFire;
  }
}
