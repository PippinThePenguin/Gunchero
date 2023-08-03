using System;
using System.Collections.Generic;
using UnityEngine;

//Creates new coins and modifies them based on level status
public class CoinManager : MonoBehaviour {
  public static Action<Coin> OnCoinPickStarted;
  public static Action<Coin> OnCoinPickComplete;

  private List<Coin> _activeCoins;

  private void OnEnable() {
    LevelFlow.OnEnemyDeath += SpawnCoin;
    LevelFlow.OnLevelClear += CollectAll;
  }

  private void OnDisable() {
    LevelFlow.OnEnemyDeath -= SpawnCoin;
    LevelFlow.OnLevelClear -= CollectAll;
  }  

  private void SpawnCoin(Transform point) {
    Transform newCoin = Spawner.Instance.Spawn("Coin", point.position, point.rotation);
    if (newCoin == null) {
      return;
    }
    newCoin.gameObject.SetActive(true);
  }

  private void CollectAll() {
    var player = FindObjectOfType<CharacterMover>();
    _activeCoins = new List<Coin>(FindObjectsOfType<Coin>());
    foreach (Coin coin in _activeCoins) {
      if (coin != null && coin.gameObject.activeSelf) {
        StartCoroutine(coin.FlyToTarget(player.transform));
      }
    }
  }  
}

