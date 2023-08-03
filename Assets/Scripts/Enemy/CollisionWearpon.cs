using System.Collections;
using UnityEngine;

//Deals damage to colliding objects of corresponding layer. Has cooldown.
public class CollisionWearpon : MonoBehaviour {
  [SerializeField] private LayerMask _damageble;
  [SerializeField] private int _damge = 1;
  [SerializeField] private float _cooldown = 1f;

  private bool _canDamage = true;

  private void OnEnable() {
    _canDamage = true;
  }

  private void OnCollisionStay(Collision collision) {
    if (_canDamage && (_damageble == (_damageble | (1 << collision.gameObject.layer)))) {
      var targetHealth = collision.transform.GetComponent<IDamagable>();
      if (targetHealth != null) {
        targetHealth.TakeDamage(_damge);
        _canDamage = false;
        StartCoroutine(Cooldown());
      }
    }
  }

  private IEnumerator Cooldown() {
    yield return new WaitForSeconds(_cooldown);
    _canDamage = true;
  }
}
