using Event;
using Game.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using State = Game.Player.State;

namespace Game {
  public class PlayerController : MonoBehaviour {
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

    public CharacterController Controller { get; private set; }
    public Camera MainCamera { get; private set; }
    public PlayerData Data { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    // Called before the first frame update
    private void Awake() {
      this.Controller = this.GetComponent<CharacterController>();
      this.MainCamera = Camera.main;
      this.Agent = this.GetComponent<NavMeshAgent>();
      this.Data = this.GetComponent<PlayerData>();
      this.Data.State = State.Init(this);
      this.ConnectInputCallbacks();

      Logger.Log($"PlayerController Start - Game beginning with score: {this.Data.Score}");
    }

    // Called once per frame
    private void Update() {
      this.Data.State.Update();
    }

    private void OnEnable() {
      InputManager.Actions.Master.Enable();
    }

    private void OnDisable() {
      InputManager.Actions.Master.Disable();
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

    private void ConnectInputCallbacks() {
      InputManager.Actions.Master.MoveToCursor.canceled += this.MoveToCursor;
      InputManager.Actions.Master.Quit.performed += this.Quit;
    }

    private void Quit(InputAction.CallbackContext ctx) {
      Logger.Log("Quit");
      Application.Quit();
    }

    private void MoveToCursor(InputAction.CallbackContext ctx) {
      Logger.Log("MoveToCursorCanceled");
      this.SpawnClickMarker();
      this.SetMoveTarget();
    }

    private RaycastHit? GetClickHit() {
      var ray = this.MainCamera.ScreenPointToRay(Input.mousePosition);
      if (!Physics.Raycast(ray, out var hit, 100.0f, this.clickableLayers)) {
        return null;
      }

      return hit;
    }

    private void SpawnClickMarker() {
      Logger.Log("SpawnClickMarker");
      if (this.GetClickHit() is { } hit) {
        if (Application.isEditor) {
          var ray = this.MainCamera.ScreenPointToRay(Input.mousePosition);
          Debug.DrawLine(ray.origin, hit.point, Color.green, 0.25f);
        }

        if (this.clickEffect) {
          Instantiate(
            this.clickEffect,
            hit.point += Vector3.up * 0.01f,
            this.clickEffect.transform.rotation
          );
        }
      } else {
        Logger.Log("Click marker hit missed");
      }
    }

    private void SetMoveTarget() {
      Logger.Log("SetMoveTarget");
      if (this.GetClickHit() is { } hit) {
        var distance = Vector3.Distance(this.transform.position, hit.point);
        if (distance < this.Data.stopDistance) {
          Logger.Log("Move target too close");
          return;
        }

        this.Agent.destination = hit.point;
      } else {
        Logger.Log("Move target hit missed");
      }
    }
  }
}