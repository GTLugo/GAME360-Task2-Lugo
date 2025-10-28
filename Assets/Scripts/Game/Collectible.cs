using UnityEngine;

public class Collectible : MonoBehaviour
{
  [Header("Collectible Settings")]
  public int scoreValue = 10;
  public float rotationSpeed = 100f;

  void Start()
  {
    Debug.Log("Collectible created: " + gameObject.name + " worth " + scoreValue + " points");
  }

  void Update()
  {
    // Rotate the collectible for visual appeal
    transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
  }
}