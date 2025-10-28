using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Header("Movement Settings")]
  public float moveSpeed = 5f;
  public float turnTime = 0.1f;
  float turnSpeed;

  [Header("Components")]
  public CharacterController controller;

  [Header("Game Stats")]
  public int Score { get; private set; }

  // Called before the first frame update
  void Start()
  {
    Debug.Log("PlayerController Start - Game beginning with score: " + Score);
  }

  // Called once per frame
  void Update()
  {
    HandleMovement();
  }

  // New movement handling based on Brackeys' Third Person Movement tutorial: https://youtu.be/4HpC--2iowE
  void HandleMovement()
  {
    // Get input from keyboard
    float horizontal = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right arrows
    float vertical = Input.GetAxisRaw("Vertical");     // W/S or Up/Down arrows

    // Create movement vector
    Vector3 direction = Quaternion.AngleAxis(45, Vector3.up) * new Vector3(horizontal, 0f, vertical).normalized;
    

    if (direction.magnitude >= 0.1f)
    {
      float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
      float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, turnTime);
      transform.rotation = Quaternion.Euler(0f, angle, 0f);

      controller.Move(direction * moveSpeed * Time.deltaTime);
    }
  }

  // Public method to add score
  public void AddScore(int points)
  {
    Score += points;
    Debug.Log("SCORE UPDATED: " + Score);
  }
}