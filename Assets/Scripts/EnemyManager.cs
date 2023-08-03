using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spawns enemies and looks over them
public class EnemyManager : MonoBehaviour {
  [Serializable]
  private struct EnemyType {
    public string Name;
    public int Amount;
  }

  [SerializeField] private EnemyType[] _levelEnemies;
  [SerializeField] private Transform _levelBase;
  [SerializeField] private float _enemyRadius = 0.5f;

  private Vector3[] _borders = new Vector3[4];
  private List<Transform> _activeEnemies;

  private void OnEnable() {
    LevelFlow.OnLevelSpawn += Init;
    LevelFlow.OnEnemyDeath += RemoveEnemy;
  }

  private void OnDisable() {
    LevelFlow.OnLevelSpawn -= Init;
    LevelFlow.OnEnemyDeath -= RemoveEnemy;
  }

  public void Init() {
    CalculateBorders();
    FindActiveEnemies();
    SpawnEnemies();
  }

  //Spawns Enemies Randomly inside Borders
  private void SpawnEnemies() {
    foreach (EnemyType enemyType in _levelEnemies) {
      int spawned = 0;
      while (spawned < enemyType.Amount) {
        Vector3 spawnpos = RandomPosition();
        Transform newEnemy = Spawner.Instance.Spawn(enemyType.Name, spawnpos, Quaternion.identity);
        newEnemy.gameObject.SetActive(true);
        _activeEnemies.Add(newEnemy);
        spawned++;
      }
    }
  }

  //Finds all active Enemies in Scene by EnemyHealthController
  private void FindActiveEnemies() {
    _activeEnemies = new List<Transform>();
    List<EnemyHealthController> health = new List<EnemyHealthController>(
        FindObjectsOfType<EnemyHealthController>());

    foreach (EnemyHealthController healthController in health) {
      _activeEnemies.Add(healthController.transform);
    }
  }

  //Returns Random Free position in Borders
  private Vector3 RandomPosition() {
    bool ready = false;
    Vector3 position = new Vector3();
    while (!ready) {
      position = new Vector3(UnityEngine.Random.Range(_borders[0].x, _borders[1].x), 0f, 
                             UnityEngine.Random.Range(_borders[0].z, _borders[2].z));

      Collider[] colliders = Physics.OverlapSphere(position, _enemyRadius);
      if (colliders.Length == 0) {
        ready = true;
      }
    }
    return position;
  }

  //Setups Spawn Borders
  private void CalculateBorders() {
    Vector3 center = _levelBase.position;
    float height = _levelBase.localScale.z - 2 * _enemyRadius;
    float width = _levelBase.localScale.x - 2 * _enemyRadius;

    _borders[0] = new Vector3(center.x - width / 2, 0, center.z - height / 6);
    _borders[1] = new Vector3(center.x + width / 2, 0, center.z - height / 6);
    _borders[2] = new Vector3(center.x - width / 2, 0, center.z + height / 2);
    _borders[3] = new Vector3(center.x + width / 2, 0, center.z + height / 2);

  }

  //Removes enemy from list, checks if any left
  private void RemoveEnemy(Transform enemyToRemove) {
    if (!_activeEnemies.Contains(enemyToRemove)) {
      Debug.LogWarning("Unlisted Enemy!");
      return;
    }
    _activeEnemies.Remove(enemyToRemove);
    if (_activeEnemies.Count == 0) {
      StartCoroutine(ClearLevel());      
    }
  }

  private IEnumerator ClearLevel() {
    yield return null;
    LevelFlow.OnLevelClear?.Invoke();
  }
}
