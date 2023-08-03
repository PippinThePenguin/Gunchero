using System.Collections;
using UnityEngine;
using TMPro;

//Countdown before gameplay start
public class CountDown : MonoBehaviour {
  [SerializeField] private TMP_Text _textBox;

  public void StartCountDown() {
    StartCoroutine(Counting());
  }

  private IEnumerator Counting() {
    _textBox.text = "3";
    float time = 3f;
    while (time > 0) {
      yield return null;
      time -= Time.deltaTime;
      _textBox.text = Mathf.RoundToInt(time).ToString();
    }
    gameObject.SetActive(false);
  }
}
