using UnityEngine;

//Level Exit door
public class ExitController : MonoBehaviour {
  [SerializeField] private ParticleSystem _particles;

  private BoxCollider _collider;

  private void Awake() {
    _collider = GetComponent<BoxCollider>();
    _collider.enabled = false;
  }

  private void OnEnable() {
    LevelFlow.OnLevelClear += OpenExit;
  }

  private void OnDisable() {
    LevelFlow.OnLevelClear -= OpenExit;
  }

  private void OnTriggerEnter(Collider other) {
    LevelFlow.OnLevelEnd?.Invoke();
  }

  private void OpenExit() {
    _collider.enabled = true;
    if (_particles != null) {
      _particles.gameObject.SetActive(true);
    }
  }
}
