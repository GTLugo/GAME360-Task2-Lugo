using UnityEngine;

public class SceneManager : MonoBehaviour
{
  public static SceneManager Instance;

  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
      Debug.LogError("Extra scene manager");
    }
  }

  void Start()
  {

  }

  void Update()
  {

  }
}
