using Managers;
using Managers.Global;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Character.Enemy {
  public class EnemyController : MonoBehaviour {
    public float lookRadius = 5f;

    private NavMeshAgent _agent;
    private CharacterCombat _combat;
    private CharacterStats _stats;
    private Transform _target;

    private void Start() {
      this._stats = this.GetComponent<CharacterStats>();
      this._agent = this.GetComponent<NavMeshAgent>();
      this._combat = this.GetComponent<CharacterCombat>();
      this._target = PlayerManager.Instance.player.transform;
    }

    private void Update() {
      var distance = Vector3.Distance(this._target.position, this.transform.position);
      if (distance <= this.lookRadius) {
        this._agent.destination = this._target.position;

        if (distance <= this._agent.stoppingDistance + 0.5f) {
          // Attack
          var targetStats = this._target.GetComponent<CharacterStats>();
          if (targetStats) {
            if (this._combat.TryAttack(targetStats)) {
              EventManager.playerHurt.Trigger((targetStats.Health, targetStats.MaxHealth));
            }
          }
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