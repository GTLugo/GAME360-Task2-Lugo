using UnityEngine;

namespace Managers {
  // Adapted from https://gamedev.stackexchange.com/a/151547/183365
  public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    public static T Instance { get; private set; }

    private void Awake() {
      if (Instance is null) {
        var instances = FindObjectsByType<T>(FindObjectsSortMode.None);
        switch (instances.Length) {
          case 0:
            Logger.LogWarning($"No singleton `{typeof(T)}` found. Creating one from scratch.");
            Instance = new GameObject($"{typeof(T)}").AddComponent<T>();
            break;
          default:
            Instance = instances[0];

            if (instances.Length > 1) {
              Logger.LogError($"Destroying extra {instances.Length} singletons of `{typeof(T).Name}`");
              foreach (var i in instances) {
                Destroy(i.gameObject);
              }
            }

            break;
        }

        if (instances.Length == 1) { }
      } else {
        Destroy(this.gameObject);
        Logger.LogError($"Extra Singleton `{typeof(T).Name}`");
      }
    }
  }
}