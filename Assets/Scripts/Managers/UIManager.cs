using TMPro;
using UnityEngine;

namespace Managers {
  public class UIManager : Singleton<UIManager> {
    [Header("UI Objects")]
    public GameObject scoreObject;

    public GameObject wonObject;

    public void UpdateScore(int score) {
      if (!this.scoreObject) {
        Logger.LogError("Score Object was null");
        return;
      }

      var scoreText = this.scoreObject.GetComponent<TextMeshProUGUI>();

      if (!scoreText) {
        Logger.LogError("Score Text on Score Object was null");
        return;
      }

      scoreText.text = $"Score: {score}";
    }

    public void Won(Vector3 playerPosition) {
      if (!this.wonObject) {
        return;
      }

      this.wonObject.SetActive(true);
    }
  }
}