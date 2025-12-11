using Managers.Global;
using TMPro;
using UnityEngine;

namespace Managers {
  public class UIManager : Singleton<UIManager> {
    [Header("UI Objects")]
    public GameObject scoreObject;

    public GameObject wonObject;
    public GameObject lostObject;

    private void OnEnable() {
      EventManager.scoreChanged.Subscribe(this.UpdateScore);
      EventManager.playerWon.Subscribe(this.PlayerWon);
      EventManager.playerDied.Subscribe(this.PlayerLost);
    }

    private void OnDisable() {
      EventManager.scoreChanged.Unsubscribe(this.UpdateScore);
      EventManager.playerWon.Unsubscribe(this.PlayerWon);
      EventManager.playerDied.Unsubscribe(this.PlayerLost);
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
      this.wonObject?.SetActive(true);
    }

    private void PlayerLost(Vector3 playerPosition) {
      this.lostObject?.SetActive(true);
    }
  }
}