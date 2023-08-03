using System;
using System.Collections;
using UnityEngine;

//Alternating agent behavior between moving and firing.
public class EnemyStateController : MonoBehaviour, IBehaviourController {
  public event Action<bool> OnStatusChange;

  public bool CanFire { get; private set; } = true;

  [SerializeField] private float _moveTime = 1f;
  [SerializeField] private float _ShootTime = 2f;

  private void OnEnable() {
    OnStatusChange += StartCycle;
    LevelFlow.OnLevelStart += Init;
  }

  private void OnDisable() {
    OnStatusChange -= StartCycle;
    LevelFlow.OnLevelStart -= Init;
  }

  public void Init() {
    OnStatusChange?.Invoke(CanFire);    
  }
  
  private void StartCycle(bool canFire) {
    if (canFire) {
      StartCoroutine(StateCycle(_ShootTime));
    } else {
      StartCoroutine(StateCycle(_moveTime));
    }
  }

  private IEnumerator StateCycle(float time) {
    float enlapsedTime = 0f;
    while (enlapsedTime < time) {
      yield return null;
      enlapsedTime += Time.deltaTime;
    }
    CanFire = !CanFire;
    OnStatusChange?.Invoke(CanFire);
  }
}
