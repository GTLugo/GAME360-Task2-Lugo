using UnityEngine;

namespace Managers {
  // Adapted from https://gamedev.stackexchange.com/a/151547/183365
  public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T s_instance;

    public static T Instance {
      get => s_instance ??= FindSingleton();
      private set => s_instance = value;
    }

    protected void Awake() {
      Instance = FindSingleton();
    }

    private static T FindSingleton() {
      T instance;

      var instances = FindObjectsByType<T>(FindObjectsSortMode.None);
      switch (instances.Length) {
        case 0:
          Logger.LogWarning($"No singleton `{typeof(T)}` found. Creating one from scratch.");
          instance = new GameObject($"{typeof(T)}").AddComponent<T>();
          break;
        default:
          instance = instances[0];

          if (instances.Length > 1) {
            Logger.LogError($"Destroying extra {instances.Length} singletons of `{typeof(T).Name}`");
            foreach (var i in instances) {
              Destroy(i.gameObject);
            }
          }

          break;
      }

      return instance;
    }
  }
}