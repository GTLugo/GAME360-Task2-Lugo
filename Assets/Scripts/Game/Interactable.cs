using UnityEngine;

namespace Game {
  public class Interactable : MonoBehaviour {
    public float radius = 1f;

    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.cyan;
      Gizmos.DrawWireSphere(this.transform.position, this.radius);
    }
  }
}