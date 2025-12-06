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
        Destroy(this.gameObject);
        Logger.LogError($"Extra {this.GetType().Name}");
      }
    }

    public void StartGame() {
      SceneManager.LoadScene(this.gameScene);
    }

    public void QuitGame() {
      Application.Quit();
    }
  }
}