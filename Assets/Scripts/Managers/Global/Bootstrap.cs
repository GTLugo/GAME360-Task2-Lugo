using UnityEngine;

namespace Managers.Global {
  public static class Bootstrap {
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Run() {
      Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("GlobalManagers")));
    }
  }

  public static class AnimationLibrary {
    public static readonly int isWalking = Animator.StringToHash("isWalking");
    public static readonly int hasDied = Animator.StringToHash("hasDied");
    public static readonly int wasRevived = Animator.StringToHash("wasRevived");
    public static readonly int isDodging = Animator.StringToHash("isDodging");
    public static readonly int attacked = Animator.StringToHash("attacked");
    public static readonly int isFloating = Animator.StringToHash("isFloating");
  }
}