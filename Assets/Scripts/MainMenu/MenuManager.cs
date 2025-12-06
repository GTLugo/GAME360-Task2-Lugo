using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu {
  public class MenuManager : MonoBehaviour {
    private static MenuManager s_self;

    public string gameScene;

    private void Awake() {
      if (s_self == null) {
        s_self = this;
      } else {
        Destroy(gameObject);
        Logger.LogError($"Extra {GetType().Name}");
      }
    }

    public void StartGame() {
      SceneManager.LoadScene(gameScene);
    }

    public void QuitGame() {
      Application.Quit();
    }
  }
}