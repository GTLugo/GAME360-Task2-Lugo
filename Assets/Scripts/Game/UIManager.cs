using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public static UIManager self;

  public GameObject scoreObject;
  private TextMeshProUGUI scoreText;

  public GameObject playerObject;
  private PlayerController playerController;

  void Awake()
  {
    if (self == null)
    {
      self = this;
    }
    else
    {
      Destroy(gameObject);
      Debug.LogError("Extra ui manager");
    }

    scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
    playerController = playerObject.GetComponent<PlayerController>();

    Debug.Log(scoreText);
    Debug.Log(playerController);
  }

  void Start()
  {
  }

  void Update()
  {
    scoreText.text = "Score: " + playerController.Score;
  }
}
