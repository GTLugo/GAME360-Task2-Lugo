using UnityEngine;

public class UIManager : MonoBehaviour
{
  public static UIManager Instance;

  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
      Debug.LogError("Extra ui manager");
    }
  }

  void Start()
  {

  }

  void Update()
  {

  }
}
