using Managers.Global;
using UnityEngine;

namespace Game {
  public class Button : MonoBehaviour {
    private static readonly int emissionColor = Shader.PropertyToID("_EmissionColor");
    public int id = -1;
    public Material buttonMaterial;

    private bool _wasUsed;

    private void Start() {
      this.buttonMaterial.SetColor(emissionColor, Color.aquamarine);
    }

    public void OnInteract(Interactable interactable) {
      if (this._wasUsed) {
        return;
      }

      this._wasUsed = true;
      this.buttonMaterial.SetColor(emissionColor, Color.red);
      EventManager.buttonPushed.Trigger((this.id, interactable.transform.position));
      Logger.Log("Click!");
    }
  }
}