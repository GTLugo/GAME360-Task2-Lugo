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
      direction = Quaternion.AngleAxis(45, Vector3.up) * new Vector3(horizontal, 0f, vertical).normalized;
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

    private State _state;
    public int Score { get; private set; }

    // Called before the first frame update
    private void Start() {
      Transition<IdleState>();
      Logger.Log("PlayerController Start - Game beginning with score: " + Score);
    }

    // Called once per frame
    private void Update() {
      _state.Update(new PlayerInput());
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
      AddScore(coin.scoreValue);

      // Trigger events
      if (collectedCoin != null) {
        collectedCoin.Trigger(transform.position);
      }

      if (scoreChanged != null) {
        scoreChanged.Trigger(Score);
      }

      // Log collection
      Debug.Log("COLLECTED: " + gameObject.name + " for " + coin.scoreValue + " points!");

      // Destroy the coin
      Destroy(other.gameObject);
    }

    public void Transition<T>() where T : State, new() {
      _state = new T();
      _state.player = this;
      _state.Enter();
    }

    // Public method to add score
    private void AddScore(int points) {
      Score += points;
      Logger.Log("SCORE UPDATED: " + Score);
    }
  }
}