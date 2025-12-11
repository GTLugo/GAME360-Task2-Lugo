using Managers;
using Managers.Global;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Character.Enemy {
  public class EnemyController : MonoBehaviour {
    public float lookRadius = 5f;

    private NavMeshAgent _agent;
    private CharacterStats _stats;
    private Transform _target;

    private void Start() {
      this._stats = this.GetComponent<CharacterStats>();
      this._agent = this.GetComponent<NavMeshAgent>();
      this._target = PlayerManager.Instance.player.transform;
    }

    private void Update() {
      var distance = Vector3.Distance(this._target.position, this.transform.position);
      if (distance <= this.lookRadius) {
        this._agent.destination = this._target.position;

        if (distance <= this._agent.stoppingDistance) {
          // Attack
        }
      }

      if (this._stats.Health <= 0.0f) {
        EventManager.enemyKilled.Trigger(this.transform.position);
        Destroy(this.gameObject);
      }
    }

    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(this.transform.position, this.lookRadius);
      // Gizmos.color = Color.magenta;
      // Gizmos.DrawWireSphere(this.transform.position, this._agent.stoppingDistance);
    }

    public void OnInteract(Interactable interactable) {
      var player = PlayerManager.Instance.player;
      if (!player) {
        return;
      }

      var playerCombat = player.GetComponent<CharacterCombat>();

      if (!playerCombat || !this._stats) {
        return;
      }

      if (playerCombat.TryAttack(this._stats)) {
        EventManager.playerAttacked.Trigger(player.transform.position);
      }
    }
  }
}