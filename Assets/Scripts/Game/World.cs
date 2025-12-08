using Event;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game {
  public class World : MonoBehaviour, IPointerClickHandler {
    [SerializeField]
    private GameEvent<Vector3> clickEvent;

    public void OnPointerClick(PointerEventData eventData) {
      Logger.Log($"Clicked `{eventData}");
      this.clickEvent?.Trigger(eventData.pointerPressRaycast.worldPosition);
    }
  }
}