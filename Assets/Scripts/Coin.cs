using System.Collections;
using UnityEngine;

//Single coin pickup
public class Coin : MonoBehaviour {
  private float _flyTime = 1f;
  private bool _canPick = true;

  private void OnEnable() {
    _canPick = true;
  }  

  private void OnTriggerEnter(Collider other) {
    if (!_canPick) {
      return;
    }
    StartCoroutine(FlyToTarget(other.transform));
  }

  public IEnumerator FlyToTarget(Transform target) {
    _canPick = false;
    CoinManager.OnCoinPickStarted?.Invoke(this);
    float enlapsedTime = 0f;
    while (enlapsedTime < _flyTime) {
      transform.position = Vector3.Lerp(transform.position, target.position, enlapsedTime/_flyTime);
      yield return null;
      enlapsedTime += Time.deltaTime;      
    }
    CoinManager.OnCoinPickComplete?.Invoke(this);
    gameObject.SetActive(false);
  }
}
