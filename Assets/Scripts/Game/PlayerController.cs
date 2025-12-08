using Event;
using Game.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using State = Game.Player.State;

namespace Game {
  public class PlayerController : MonoBehaviour {
    [Header("Movement")]
    [DoNotSerialize]
    public float yawVelocity;

    [SerializeField]
    private ParticleSystem clickEffect;

    [SerializeField]
    private LayerMask clickableLayers;

    [Header("Components")]
    public Animator animator;

    [Header("Events")]
    public GameEvent<int> scoreChanged;

    public GameEvent<Vector3> collectedCoin;
    public GameEvent<Vector3> won;

    public InputActions InputActions { get; private set; }
    public CharacterController Controller { get; private set; }
    public PlayerData Data { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    // Called before the first frame update
    private void Awake() {
      this.InputActions = new InputActions();
      this.InputActions.Master.Move.performed += this.ClickToMove;
      this.Controller = this.GetComponent<CharacterController>();
      this.Data = this.GetComponent<PlayerData>();
      this.Data.State = State.Init(this);
      this.Agent = this.GetComponent<NavMeshAgent>();

      Logger.Log($"PlayerController Start - Game beginning with score: {this.Data.Score}");
    }

    // Called once per frame
    private void Update() {
      if (Input.GetKeyDown(KeyCode.Escape)) {
        Application.Quit();
      }

      this.Data.State.Update();
    }

    private void OnEnable() {
      this.InputActions.Enable();
    }

    private void OnDisable() {
      this.InputActions.Disable();
    }

    // Called when another collider enters this trigger collider
    private void OnTriggerEnter(Collider other) {
      // Check if a coin touched the player
      if (!other.CompareTag("Coin")) {
        return;
      }

      if (!other.TryGetComponent<Collectible>(out var coin)) {
        return;
      }

      // Add score to player
      this.Data.Score += coin.scoreValue;
      Logger.Log($"SCORE UPDATED: {this.Data.Score}");

      // Trigger events
      this.collectedCoin?.Trigger(coin.transform.position);
      this.scoreChanged?.Trigger(this.Data.Score);

      // Log collection
      Debug.Log($"COLLECTED: {this.gameObject.name} for {coin.scoreValue} points!");

      // Destroy the coin
      Destroy(other.gameObject);
    }

    private void ClickToMove(InputAction.CallbackContext ctx) {
      var mainCamera = Camera.main;
      if (!mainCamera) {
        return;
      }

      if (!Physics.Raycast(
            mainCamera.ScreenPointToRay(Input.mousePosition),
            out var hit,
            100.0f,
            this.clickableLayers
          )) {
        return;
      }

      this.Agent.destination = hit.point;
      if (this.clickEffect) {
        Instantiate(this.clickEffect, hit.point += Vector3.up * 0.1f, this.clickEffect.transform.rotation);
      }
    }

    public void FaceTarget() {
      var direction = this.Agent.desiredVelocity.normalized;
      var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
      this.transform.rotation = Quaternion.Slerp(
        this.transform.rotation,
        lookRotation,
        Time.deltaTime * this.yawVelocity
      );
    }
  }
}