using UnityEngine;

namespace Managers {
  [CreateAssetMenu(fileName = "Scene ID", menuName = "New Scene ID", order = 0)]
  public class SceneID : ScriptableObject {
    public string id;
  }
}