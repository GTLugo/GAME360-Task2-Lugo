using UnityEngine;

namespace Game.Character {
  public class CharacterStats : MonoBehaviour {
    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    public int Health { get; set; } = 100;

    [field: SerializeField]
    public int MaxHealth { get; set; } = 100;

    [field: SerializeField]
    public float AttackSpeed { get; set; } = 1f;

    [field: SerializeField]
    public float AttackCooldown { get; set; }

    [field: SerializeField]
    public int Damage { get; set; } = 30;
  }
}