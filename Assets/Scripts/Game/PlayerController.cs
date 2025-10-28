using UnityEngine;

public class PlayerInput
{
  public Vector3 direction;

  public PlayerInput()
  {
    // Get input from keyboard
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      Application.Quit();
    }

    float horizontal = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right arrows
    float vertical = Input.GetAxisRaw("Vertical");     // W/S or Up/Down arrows

    // Create movement vector
    direction = Quaternion.AngleAxis(45, Vector3.up) * new Vector3(horizontal, 0f, vertical).normalized;
  }
}

public class PlayerController : MonoBehaviour
{
  [Header("Movement Settings")]
  public float moveSpeed = 5f;
  public float turnTime = 0.1f;
  public float turnSpeed;

  [Header("Components")]
  public CharacterController controller;

  [Header("Game Stats")]
  public State state;
  public int Score { get; private set; }
  public int targetScore = 200;
  public GameEventInt scoreChanged;
  public GameEventVector collectedCoin;
  public GameEventVector won;

  // Called before the first frame update
  void Start()
  {
    state = new IdleState(this);
    Debug.Log("PlayerController Start - Game beginning with score: " + Score);
  }

  // Called once per frame
  void Update()
  {
    state.Update(new());
  }

  public void Transition(State next) {
    next.Enter();
    state = next;
  }

  // Public method to add score
  void AddScore(int points)
  {
    Score += points;
    Debug.Log("SCORE UPDATED: " + Score);
  }

  // Called when another collider enters this trigger collider
  void OnTriggerEnter(Collider other)
  {
    // Check if a coin touched the player
    if (other.CompareTag("Coin"))
    {
      // Get the PlayerController component

      if (other.TryGetComponent<Collectible>(out var coin))
      {
        // Add score to player
        AddScore(coin.scoreValue);

        // Trigger events
        if (collectedCoin != null)
        {
          collectedCoin.Trigger(transform.position);
        }

        if (scoreChanged != null)
        {
          scoreChanged.Trigger(Score);
        }

        // Log collection
        Debug.Log("COLLECTED: " + gameObject.name + " for " + coin.scoreValue + " points!");

        // Destroy the coin
        Destroy(other.gameObject);
      }
    }
  }
}