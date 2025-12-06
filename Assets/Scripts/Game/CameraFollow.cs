using UnityEngine;

namespace Game {
  public class CameraFollow : MonoBehaviour {
    [Header("Camera Settings")] public Transform target; // The player to follow
    public Vector3 offset = new(0, 5, -10);
    public float smoothSpeed = 0.125f;

    private void Start() {
      // Check if target is assigned
      if (!target) {
        Logger.LogWarning("Camera target not set! Please assign the Player.");
      } else {
        Logger.Log($"Camera following: {target.name}");
      }
    }

    // LateUpdate is called after all Update functions
    private void LateUpdate() {
      if (!target) {
        return;
      }

      // Calculate desired position
      var desiredPosition = target.position + offset;

      // Smoothly move to that position
      var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
      transform.position = smoothedPosition;

      // Look at the target
      transform.LookAt(target);
    }
  }
}