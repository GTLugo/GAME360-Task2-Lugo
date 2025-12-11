using UnityEngine;

namespace Game.Character {
  [RequireComponent(typeof(CharacterStats))]
  public class CharacterCombat : MonoBehaviour {
    private CharacterStats _stats;

    private void Start() {
      this._stats = this.GetComponent<CharacterStats>();
    }

    private void Update() {
      this._stats.AttackCooldown -= Time.deltaTime;
    }

    public bool TryAttack(CharacterStats targetStats) {
      if (this._stats.AttackCooldown > 0.0f) {
        return false;
      }

      Logger.Log($"Hit {targetStats.Name} for `{this._stats.Damage}` damage");
      targetStats.Health -= this._stats.Damage;
      this._stats.AttackCooldown = 1f / this._stats.AttackSpeed;
      return true;
    }
  }
}