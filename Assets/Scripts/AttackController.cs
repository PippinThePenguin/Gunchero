using System;
using System.Collections;
using UnityEngine;

//проверить наслледуемость

//Looks at target and attacks it
[RequireComponent(typeof(IBehaviourController))]
public class AttackController : MonoBehaviour {
  public event Action OnAttackStart;

  [SerializeField] protected float _rotationSpeed = 100f;
  [SerializeField] protected float _attackSpeed = 0.5f;
  [SerializeField] protected int _attackDamage = 1;
  [SerializeField] protected AimController _aimer;
  [SerializeField] protected Transform _body;
  [SerializeField] protected Transform _gunTip;
  [SerializeField] protected bool _animationLinked = false;

  protected IBehaviourController _statusController;
  protected IEnumerator _attackCoroutine;
  protected bool _canAttack;
  
  protected void Awake() {
    if (_statusController == null) {
      _statusController = GetComponent<IBehaviourController>();
    }      
  }

  protected void OnEnable() {
    _canAttack = false;
    _attackCoroutine = WaitAndAttack();
    _statusController.OnStatusChange += StateChager;
  }

  protected void OnDisable() {
    _statusController.OnStatusChange -= StateChager;
  }

  protected void Update() {
    FollowLook();
  }

  protected void FollowLook() {
    if (!_canAttack) {
      return;
    }

    _aimer.UpdateTarget();
    if (_aimer.Target == null) {
      return;
    }
    Quaternion targetRotation = Quaternion.LookRotation(_aimer.Target.position - _body.position);
    _body.rotation = Quaternion.RotateTowards(_body.rotation, targetRotation,
                                              _rotationSpeed * Time.deltaTime);
  }

  protected void StateChager(bool canFire) {
    _canAttack = canFire;
    if (canFire) {
      _attackCoroutine = WaitAndAttack();
      StartCoroutine(_attackCoroutine);
    } else {
      StopCoroutine(_attackCoroutine);
    }
  }

  protected IEnumerator WaitAndAttack() {
    _aimer.UpdateTarget();
    OnAttackStart?.Invoke();
    yield return new WaitForSeconds(1f / _attackSpeed);
    if (!_animationLinked) {
      Shoot();
    }
    _attackCoroutine = WaitAndAttack();
    StartCoroutine(_attackCoroutine);
  }

  protected void Shoot() {
    if (!_aimer.Target) {
      return;
    }

    Transform newBullet = Spawner.Instance.Spawn("Bullet", _gunTip.position, _gunTip.rotation);
    newBullet.gameObject.SetActive(true);
    Bullet bulletScript = newBullet.GetComponent<Bullet>();
    bulletScript.Damage = _attackDamage;
    bulletScript.Init();
  }  
}
