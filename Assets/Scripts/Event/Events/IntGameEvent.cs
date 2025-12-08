using UnityEngine;

namespace Event.Events {
  [CreateAssetMenu(menuName = "GameEvent/Int", fileName = "New Game Event")]
  public class IntGameEvent : GameEvent<int> { }
}