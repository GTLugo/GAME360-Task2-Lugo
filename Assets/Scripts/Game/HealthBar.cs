using Managers.Global;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
  public class HealthBar : MonoBehaviour {
    public Image fill;

    private void OnEnable() {
      EventManager.playerHurt.Subscribe(this.OnPlayerHurt);
    }

    private void OnDisable() {
      EventManager.playerHurt.Unsubscribe(this.OnPlayerHurt);
    }

    private void OnPlayerHurt((int current, int max) health) {
      this.fill.fillAmount = (float)health.current / health.max;
    }
  }
}