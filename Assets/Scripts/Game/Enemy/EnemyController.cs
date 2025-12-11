using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy {
  public class EnemyController : MonoBehaviour {
    public float lookRadius = 5f;

    private NavMeshAgent _agent;
    private Transform _target;

    private void Start() {
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
    }

    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(this.transform.position, this.lookRadius);
      // Gizmos.color = Color.magenta;
      // Gizmos.DrawWireSphere(this.transform.position, this._agent.stoppingDistance);
    }
  }
}