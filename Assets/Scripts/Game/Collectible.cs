using Managers.Global;
using UnityEngine;

namespace Game {
  public class Collectible : MonoBehaviour {
    [Header("Collectible Settings")]
    public int scoreValue = 10;

    public float rotationSpeed = 100f;
    public Vector3 rotationVector;

    public int targetButtonId = -1;

    public MeshRenderer meshRenderer;
    public new Collider collider;

    private void Update() {
      // Rotate the collectible for visual appeal
      this.transform.Rotate(this.rotationVector * (this.rotationSpeed * Time.deltaTime));
    }

    private void OnEnable() {
      EventManager.buttonPushed.Subscribe(this.OnButtonPushed);
    }

    private void OnDisable() {
      EventManager.buttonPushed.Unsubscribe(this.OnButtonPushed);
    }

    private void OnButtonPushed((int buttonId, Vector3 position) @event) {
      if (@event.buttonId != this.targetButtonId) {
        return;
      }

      this.meshRenderer.enabled = true;
      this.collider.enabled = true;
    }
  }
}