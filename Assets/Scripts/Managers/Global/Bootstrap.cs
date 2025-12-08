using UnityEngine;

namespace Managers.Global {
  public static class Bootstrap {
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Run() {
      Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("GlobalManagers")));
    }
  }
}