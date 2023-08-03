using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {
  public float Speed = 10f;
  public int Damage = 1;

  [SerializeField] private Rigidbody _rigidbody;

  private void Awake() {
    _rigidbody = GetComponent<Rigidbody>();
  }
  
  private void OnTriggerEnter(Collider other) {
    IDamagable damagable = other.GetComponent<IDamagable>();
    if (damagable != null) {
      damagable.TakeDamage(Damage);
    }
    LevelFlow.OnBulletHit?.Invoke(other.ClosestPoint(transform.position));
    gameObject.SetActive(false);
  }

  public void Init() {
    _rigidbody.velocity = transform.rotation * Vector3.forward * Speed;
  }
}
