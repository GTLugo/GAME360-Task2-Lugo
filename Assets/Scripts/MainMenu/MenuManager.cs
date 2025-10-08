using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
  public static MenuManager self;

  public string gameScene;

  void Awake()
  {
    if (self == null)
    {
      self = this;
    }
    else
    {
      Destroy(gameObject);
      Debug.LogError("Extra menu manager");
    }
  }

  public void StartGame()
  {
    SceneManager.LoadScene(gameScene);
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
