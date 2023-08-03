using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Contains info about current level state. Also restarts scene for now
public class LevelFlow : MonoBehaviour {
  public static event Action OnLevelSpawn;
  public static event Action OnLevelStart;
  public static event Action OnLevelPause;
  public static event Action OnLevelResume;
  public static Action OnLevelClear;
  public static Action OnLevelEnd;
  public static Action OnLevelFail;
  public static Action<Transform> OnEnemyDeath;  
  public static Action<Vector3> OnBulletHit; 

  public void BeginLevel() {
    OnLevelSpawn?.Invoke();
    StartCoroutine(StartLevel());
  }
    
  public void Pause() {
    Time.timeScale = 0f;
    OnLevelPause?.Invoke();
  }

  public void Resume() {
    OnLevelResume?.Invoke();
    Time.timeScale = 1f;
  }

  public void Restart() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  private IEnumerator StartLevel() {
    yield return new WaitForSeconds(3f);
    OnLevelStart?.Invoke();
  }

  
}
