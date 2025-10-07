using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  [Header("Camera Settings")]
  public Transform target;  // The player to follow
  public Vector3 offset = new Vector3(0, 5, -10);
  public float smoothSpeed = 0.125f;

  void Start()
  {
    // Check if target is assigned
    if (target == null)
    {
      Debug.LogWarning("Camera target not set! Please assign the Player.");
    }
    else
    {
      Debug.Log("Camera following: " + target.name);
    }
  }

  // LateUpdate is called after all Update functions
  void LateUpdate()
  {
    if (target == null) return;

    // Calculate desired position
    Vector3 desiredPosition = target.position + offset;

    // Smoothly move to that position
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    transform.position = smoothedPosition;

    // Look at the target
    transform.LookAt(target);
  }
}