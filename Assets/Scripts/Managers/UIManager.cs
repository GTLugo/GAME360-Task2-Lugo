using Managers.Global;
using TMPro;
using UnityEngine;

namespace Managers {
  public class UIManager : Singleton<UIManager> {
    [Header("UI Objects")]
    public GameObject scoreObject;

    public GameObject wonObject;

    private void OnEnable() {
      EventManager.scoreChanged.Subscribe(this.UpdateScore);
      EventManager.playerWon.Subscribe(this.PlayerWon);
    }

    private void OnDisable() {
      EventManager.scoreChanged.Unsubscribe(this.UpdateScore);
      EventManager.playerWon.Unsubscribe(this.PlayerWon);
    }

    private void UpdateScore(int score) {
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

    private void PlayerWon(Vector3 playerPosition) {
      if (!this.wonObject) {
        return;
      }

      this.wonObject.SetActive(true);
    }
  }
}