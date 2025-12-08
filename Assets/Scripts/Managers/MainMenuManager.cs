using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers {
  public class MainMenuManager : Singleton<MainMenuManager> {
    public SceneID gameScene;

    public void StartGame() {
      SceneManager.LoadScene(this.gameScene.id);
    }

    public void QuitGame() {
      Application.Quit();
    }
  }
}