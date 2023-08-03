using UnityEngine;

//Spawns player's character at the beginning of the level
public class PlayerSpawn : MonoBehaviour {
  [SerializeField] private Transform _ground;
  [SerializeField] private GameObject _playerPrefab;
  [SerializeField] private float _playerRadius;

  private void OnEnable() {
    LevelFlow.OnLevelSpawn += SpawnPlayer;
  }

  private void SpawnPlayer() {
    Vector3 spawnPosition = new Vector3(_ground.position.x, 0f, 
        _ground.position.z - _ground.localScale.z / 2 + _playerRadius);
    GameObject player = Instantiate(_playerPrefab);
    player.transform.position = spawnPosition;    
  }
}
