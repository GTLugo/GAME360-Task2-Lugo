using TMPro;
using UnityEngine;

namespace Game {
  public class UIManager : MonoBehaviour {
    private static UIManager s_self;

    public GameObject scoreObject;
    public GameObject wonObject;
    private TextMeshProUGUI _scoreText;

    private void Awake() {
      if (!s_self) {
        s_self = this;
      } else {
        Destroy(this.gameObject);
        Logger.LogError($"Extra {this.GetType().Name}");
      }

      this._scoreText = this.scoreObject.GetComponent<TextMeshProUGUI>();

      Logger.Log(this._scoreText);
    }

    public void UpdateScore(int value) {
      this._scoreText.text = $"Score: {value}";
    }

    public void Won(Vector3 _) {
      this.wonObject.SetActive(true);
    }
  }
}