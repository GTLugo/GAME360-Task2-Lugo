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
    public PlayerInput PlayerInput { get; set; }

    private InputDevice _lastDevice;

    public static ControlScheme CurrentControlScheme { get; private set; }


    public static InputActions Actions => s_actions ??= new InputActions();

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

    public static Vector2 GetMoveVector() {
      return CurrentControlScheme switch {
        ControlScheme.Keyboard => Actions.Master.MoveMouse.ReadValue<Vector2>(),
        ControlScheme.Gamepad => Actions.Master.MoveGamepad.ReadValue<Vector2>(),
        _ => Actions.Master.MoveMouse.ReadValue<Vector2>()
      };
      ;
    }
  }
}