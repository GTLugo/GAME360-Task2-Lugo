using UnityEngine;
using UnityEngine.Events;

namespace Game {
  public class Interactable : MonoBehaviour {
    public float radius = 1f;

    public UnityEvent<Interactable> @event;

    private bool _hasInteracted;

    private bool _isFocus;
    private PlayerController _player;

    private void Update() {
      if (!this._isFocus || this._hasInteracted) {
        return;
      }

      var distance = Vector3.Distance(this._player.transform.position, this.transform.position);
      if (distance > this.radius + this._player.Agent.stoppingDistance) {
        return;
      }

      this._hasInteracted = true;
      this.@event?.Invoke(this);
    }

    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.cyan;
      Gizmos.DrawWireSphere(this.transform.position, this.radius);
    }

    public void OnFocused(PlayerController player) {
      this._isFocus = true;
      this._player = player;
      this._hasInteracted = false;
    }

    public void OnDefocused() {
      this._isFocus = false;
      this._player = null;
      this._hasInteracted = false;
    }
  }
}