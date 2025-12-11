using Managers.Global;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
  public class HealthBar : MonoBehaviour {
    public Image fill;
    public float fillAmount = 1.0f;

    private void Update() {
      this.fill.fillAmount = this.fillAmount;
    }

    private void OnEnable() {
      EventManager.playerHurt.Subscribe(this.OnPlayerHurt);
    }

    private void OnDisable() {
      EventManager.playerHurt.Unsubscribe(this.OnPlayerHurt);
    }

    private void OnPlayerHurt((int current, int max) health) {
      Logger.Log($"{health.current} / {health.max}");
      this.fillAmount = health.current / (float)health.max;
    }
  }
}