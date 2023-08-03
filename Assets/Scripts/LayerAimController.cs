using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Finds targer in radius by LayerMask
public class LayerAimController : AimController {
  [SerializeField] protected LayerMask _targetLayers;
  [SerializeField] protected float _aimRadius = 100f;

  public override void UpdateTarget() {
    Collider[] colliders = Physics.OverlapSphere(transform.position, _aimRadius, _targetLayers);
    Transform closest = null;
    float minDistance = Mathf.Infinity;
    foreach (Collider collider in colliders) {
      float distance = (transform.position - collider.transform.position).sqrMagnitude;
      if (distance < minDistance) {
        minDistance = distance;
        closest = collider.transform;
      }
    }
    _lastTarget = closest;
  }
}
