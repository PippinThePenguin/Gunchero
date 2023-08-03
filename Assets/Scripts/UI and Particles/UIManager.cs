using UnityEngine;

//Changing ui windows
public class UIManager : MonoBehaviour {
  [SerializeField] private GameObject _inGameUI;
  [SerializeField] private GameObject _pauseUI;
  [SerializeField] private GameObject _winUI;
  [SerializeField] private GameObject _loseUI;

  private void OnEnable() {
    _inGameUI.SetActive(false);
    _pauseUI.SetActive(false);
    _winUI.SetActive(false);
    _loseUI.SetActive(false);

    LevelFlow.OnLevelPause += Pause;
    LevelFlow.OnLevelResume += Resume;
    LevelFlow.OnLevelEnd += Win;
    LevelFlow.OnLevelFail += Lose;
    LevelFlow.OnLevelStart += Resume;
  }

  private void OnDisable() {
    LevelFlow.OnLevelPause -= Pause;
    LevelFlow.OnLevelResume -= Resume;
    LevelFlow.OnLevelEnd -= Win;
    LevelFlow.OnLevelFail -= Lose;
    LevelFlow.OnLevelStart -= Resume;
  }

  private void Lose() {
    _inGameUI.SetActive(false);
    _loseUI.SetActive(true);
  }

  private void Win() {
    _inGameUI.SetActive(false);
    _winUI.SetActive(true);
  }

  private void Resume() {
    _pauseUI.SetActive(false);
    _inGameUI.SetActive(true);
  }

  private void Pause() {
    _inGameUI.SetActive(false);
    _pauseUI.SetActive(true);
  }
}
