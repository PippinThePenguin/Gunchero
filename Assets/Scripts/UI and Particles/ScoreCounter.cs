using UnityEngine;
using TMPro;

//How many coins you get
public class ScoreCounter : MonoBehaviour {
  [SerializeField] private TMP_Text _textBox;
  private int _score = 0;

  private void OnEnable() {
    CoinManager.OnCoinPickComplete += AddScore;
  }

  private void AddScore(Coin coin) {
    _score++;
    _textBox.text = _score.ToString();
  }
}
