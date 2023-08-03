using UnityEngine;

//Responsible for taking damage and dying.
public class EnemyHealthController : MonoBehaviour, IDamagable {
  public int MaxHealth => _maxHealth;
  public int CurrHealth { get; private set; }

  [SerializeField] private int _maxHealth = 2;

  private void OnEnable() {
    CurrHealth = _maxHealth;
  }

  public void TakeDamage(int ammount) {
    CurrHealth -= ammount;
    if (CurrHealth <= 0) {
      LevelFlow.OnEnemyDeath?.Invoke(transform);
      gameObject.SetActive(false);
    }
  }
}
