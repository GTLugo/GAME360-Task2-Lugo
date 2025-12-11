using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Game {
  public enum ControlScheme {
    Keyboard,
    Gamepad
  }

  public class InputManager : Singleton<InputManager> {
    private static InputActions s_actions;

    [field: SerializeField]
    private PlayerInput playerInput;

    private InputDevice _lastDevice;
    private Camera _mainCamera;

    public static ControlScheme CurrentControlScheme { get; private set; }

    public static InputActions Actions => s_actions ??= new InputActions();

    private void Start() {
      this._mainCamera = Camera.main;
    }

    private void OnEnable() {
      InputSystem.onEvent += this.OnDeviceChange;
    }

    private void OnDisable() {
      InputSystem.onEvent -= this.OnDeviceChange;
    }

    // https://discussions.unity.com/t/detect-when-device-changes-and-get-corresponding-control-scheme/905624/5
    private void OnDeviceChange(InputEventPtr eventPtr, InputDevice device) {
      if (this._lastDevice == device) {
        return;
      }

      if (eventPtr.type != StateEvent.Type) {
        return;
      }

      var validPress = eventPtr.EnumerateChangedControls(device, 0.01F).Any();

      if (!validPress) {
        return;
      }

      switch (device) {
        case Keyboard:
        case Mouse: {
          if (CurrentControlScheme == ControlScheme.Keyboard) {
            return;
          }

          CurrentControlScheme = ControlScheme.Keyboard;
          Cursor.visible = true;
          // OnControlSchemeChanged?.Invoke(CurrentControlScheme);
          break;
        }
        case Gamepad:
          if (CurrentControlScheme == ControlScheme.Gamepad) {
            return;
          }

          CurrentControlScheme = ControlScheme.Gamepad;
          Cursor.visible = false;
          // OnControlSchemeChanged?.Invoke(CurrentControlScheme);
          break;
      }

      Logger.Log($"Switching scheme to `{CurrentControlScheme}`");
    }

    public static bool IsKeyboardScheme() {
      return CurrentControlScheme == ControlScheme.Keyboard;
    }

    public static bool IsGamepadScheme() {
      return CurrentControlScheme == ControlScheme.Gamepad;
    }

    public static Vector3 GetMousePosOnPlane(Plane plane) {
      var mouseRay = Instance._mainCamera.ScreenPointToRay(Input.mousePosition);
      plane.Raycast(mouseRay, out var distance);
      var intersection = mouseRay.GetPoint(distance);

      if (Application.isEditor) {
        Debug.DrawLine(mouseRay.origin, intersection, Color.blue, 0.1f);
      }

      return intersection;
    }

    public static RaycastHit? GetMousePosInWorld(int clickableLayers) {
      var ray = Instance._mainCamera.ScreenPointToRay(Input.mousePosition);

      if (!Physics.Raycast(ray, out var hit, 100.0f, clickableLayers)) {
        return null;
      }

      return hit;
    }

    public static Vector2 GetMoveVector() {
      return Actions.Master.Move.ReadValue<Vector2>();
    }
  }
}