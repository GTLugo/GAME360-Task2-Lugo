using UnityEngine;

namespace Game {
  public class Collectible : MonoBehaviour {
    [Header("Collectible Settings")] public int scoreValue = 10;

    public float rotationSpeed = 100f;

    public Transform mesh;

    private void Start() {
      Logger.Log($"Collectible created: {this.gameObject.name} worth {this.scoreValue} points");
    }

    private void Update() {
      // Rotate the collectible for visual appeal
      this.mesh.Rotate(Vector3.left * (this.rotationSpeed * Time.deltaTime));
    }
  }
}