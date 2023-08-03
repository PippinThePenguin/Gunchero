using UnityEngine;

//Taking damage for player character.
public class PlayerHealthController : MonoBehaviour, IDamagable {
  public int MaxHealth => _maxHealth;
  public int CurrHealth { get; private set; }

  private int _maxHealth = 12;

  private void OnEnable() {
    CurrHealth = _maxHealth;
  }

  public void TakeDamage(int ammount) {
    CurrHealth -= ammount;
    if (CurrHealth <= 0) {
      LevelFlow.OnLevelFail?.Invoke();
    }
  }
}
