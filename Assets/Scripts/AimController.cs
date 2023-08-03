using System;
using UnityEngine;

//Selecting target for agent
public abstract class AimController : MonoBehaviour {
  public event Action<Transform> OnTargetChange;
  public Transform Target => _lastTarget;
  protected Transform _lastTarget;
  
  public virtual void UpdateTarget() {
    OnTargetChange?.Invoke(Target);
  }
}
