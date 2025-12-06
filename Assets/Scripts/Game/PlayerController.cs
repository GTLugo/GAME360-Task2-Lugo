using Event.Int;
using Event.Vector;
using Game.Player;
using UnityEngine;

namespace Game {
  public class PlayerInput {
    public Vector3 direction;

    public PlayerInput() {
      // Get input from keyboard
      if (Input.GetKeyDown(KeyCode.Escape)) {
        Application.Quit();
      }

      var horizontal = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right arrows
      var vertical = Input.GetAxisRaw("Vertical"); // W/S or Up/Down arrows

      // Create movement vector
      this.direction = Quaternion.AngleAxis(45, Vector3.up) * new Vector3(horizontal, 0f, vertical).normalized;
    }
  }

  public class PlayerController : MonoBehaviour {
    [Header("Movement Settings")] public float moveSpeed = 5f;

    public float turnTime = 0.1f;
    public float turnSpeed;

    [Header("Components")] public CharacterController controller;
    public int targetScore = 200;

    public GameEventInt scoreChanged;
    public GameEventVector collectedCoin;
    public GameEventVector won;

    public State State { get; set; }
    public int Score { get; private set; }

    // Called before the first frame update
    private void Start() {
      this.State = State.Init(this);
      Logger.Log("PlayerController Start - Game beginning with score: " + this.Score);
    }

    // Called once per frame
    private void Update() {
      this.State.Update(new PlayerInput());
    }

    // Called when another collider enters this trigger collider
    private void OnTriggerEnter(Collider other) {
      // Check if a coin touched the player
      if (!other.CompareTag("Coin")) {
        return;
      }

      // Get the PlayerController component
      if (!other.TryGetComponent<Collectible>(out var coin)) {
        return;
      }

      // Add score to player
      this.AddScore(coin.scoreValue);

      // Trigger events
      if (this.collectedCoin != null) {
        this.collectedCoin.Trigger(this.transform.position);
      }

      if (this.scoreChanged != null) {
        this.scoreChanged.Trigger(this.Score);
      }

      // Log collection
      Debug.Log("COLLECTED: " + this.gameObject.name + " for " + coin.scoreValue + " points!");

      // Destroy the coin
      Destroy(other.gameObject);
    }

    // Public method to add score
    private void AddScore(int points) {
      this.Score += points;
      Logger.Log("SCORE UPDATED: " + this.Score);
    }
  }
}