using UnityEngine;

namespace Game {
  public class Collectible : MonoBehaviour {
    [Header("Collectible Settings")]
    public int scoreValue = 10;

    public float rotationSpeed = 100f;
    public Vector3 rotationVector;

    public Transform mesh;

    private void Update() {
      // Rotate the collectible for visual appeal
      this.mesh.Rotate(this.rotationVector * (this.rotationSpeed * Time.deltaTime));
    }
  }
}