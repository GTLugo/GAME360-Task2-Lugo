using UnityEngine;

namespace Game {
  public class Collectible : MonoBehaviour {
    [Header("Collectible Settings")] public int scoreValue = 10;

    public float rotationSpeed = 100f;

    public Transform mesh;

    private void Start() {
      Logger.Log($"Collectible created: {gameObject.name} worth {scoreValue} points");
    }

    private void Update() {
      // Rotate the collectible for visual appeal
      mesh.Rotate(Vector3.left * (rotationSpeed * Time.deltaTime));
    }
  }
}