using Game.Player;
using Managers.Global;
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

    [SerializeField]
    private LayerMask interactableLayers;

    [Header("Components")]
    public Animator animator;

    // private Vector3 _moveTarget;

    public CharacterController Controller { get; private set; }
    private Camera MainCamera { get; set; }
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
      if (InputManager.Actions.Master.AttackInteract.triggered) {
        if (InputManager.GetMousePosInWorld(this.interactableLayers) is { } hit &&
            hit.collider.CompareTag("Interactable")) {
          EventManager.buttonPushed.Trigger((0, hit.point));
          Logger.Log("Click!");
        }
      }

      this.Data.State.Update();
    }

    private void OnEnable() {
      InputManager.Actions.Master.Enable();
      EventManager.playerDied.Subscribe(this.OnDied);
      EventManager.playerRespawned.Subscribe(this.OnRespawn);
    }

    private void OnDisable() {
      InputManager.Actions.Master.Disable();
      EventManager.playerDied.Unsubscribe(this.OnDied);
      EventManager.playerRespawned.Unsubscribe(this.OnRespawn);
    }

    // Called when another collider enters this trigger collider
    private void OnTriggerEnter(Collider other) {
      // Check if a coin touched the player
      if (other.CompareTag("Coin")) {
        this.CoinCollided(other);
      }

      if (other.CompareTag("Kill")) {
        this.KillCollided(other);
      }
    }

    public void ApplyGravity() {
      if (!this.Data.IsAlive) {
        return;
      }

      this.Agent.autoRepath = this.Controller.isGrounded;
      this.Agent.updatePosition = this.Controller.isGrounded;

      if (this.Controller.isGrounded || !this.Data.isAffectedByGravity) {
        this.Data.gravityVelocity = 0.0f;
      }

      var destination = this.Agent.destination;
      this.Agent.updatePosition = false;
      this.Data.gravityVelocity += Physics.gravity.y * Time.deltaTime * Time.deltaTime;
      this.Agent.Move(new Vector3(0.0f, this.Data.gravityVelocity, 0.0f));
      this.Agent.Warp(this.transform.position);
      this.Agent.updatePosition = true;
      this.Agent.destination = destination;
    }

    private void CoinCollided(Collider collider) {
      if (!collider.TryGetComponent<Collectible>(out var coin)) {
        return;
      }

      // Add score to player
      this.Data.Score += coin.scoreValue;
      Logger.Log($"SCORE UPDATED: {this.Data.Score}");

      // Trigger events
      EventManager.coinCollected.Trigger(coin.transform.position);
      EventManager.scoreChanged.Trigger(this.Data.Score);

      // Log collection
      Debug.Log($"COLLECTED: {this.gameObject.name} for {coin.scoreValue} points!");

      // Destroy the coin
      Destroy(coin.gameObject);
    }

    private void KillCollided(Collider collider) {
      this.Data.Health = 0;
      this.Data.Score /= 2;
      EventManager.scoreChanged.Trigger(this.Data.Score);
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

    private void SpawnClickMarker() {
      {
        if (InputManager.GetMousePosInWorld(this.interactableLayers) is { } hit &&
            hit.collider.CompareTag("Interactable")) {
          return;
        }
      }
      Logger.Log("SpawnClickMarker");

      {
        if (InputManager.GetMousePosInWorld(this.clickableLayers) is { } hit) {
          if (Application.isEditor) {
            var ray = this.MainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, hit.point, Color.green, 0.1f);
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
    }

    private void SetMoveTarget() {
      {
        if (InputManager.GetMousePosInWorld(this.interactableLayers) is { } hit &&
            hit.collider.CompareTag("Interactable")) {
          return;
        }
      }
      Logger.Log("SetMoveTarget");

      {
        if (InputManager.GetMousePosInWorld(this.clickableLayers) is { } hit) {
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

    private void OnDied(Vector3 position) {
      this.Data.IsAlive = false;
      InputManager.Actions.Master.Disable();
      this.animator.SetTrigger(AnimationLibrary.hasDied);
    }

    private void OnRespawn(Vector3 position) {
      this.animator.SetTrigger(AnimationLibrary.wasRevived);
      this.Data.Health = this.Data.MaxHealth;
      InputManager.Actions.Master.Enable();
      this.Data.IsAlive = true;
    }
  }
}