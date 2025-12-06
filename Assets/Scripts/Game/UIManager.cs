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
        Destroy(gameObject);
        Logger.LogError($"Extra {GetType().Name}");
      }

      _scoreText = scoreObject.GetComponent<TextMeshProUGUI>();

      Logger.Log(_scoreText);
    }

    public void UpdateScore(int value) {
      _scoreText.text = $"Score: {value}";
    }

    public void Won(Vector3 _) {
      wonObject.SetActive(true);
    }
  }
}