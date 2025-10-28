using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  public static UIManager self;

  public GameObject scoreObject;
  private TextMeshProUGUI scoreText;

  public GameObject wonObject;

  void Awake()
  {
    if (self == null)
    {
      self = this;
    }
    else
    {
      Destroy(gameObject);
      Debug.LogError("Extra " + GetType().Name);
    }

    scoreText = scoreObject.GetComponent<TextMeshProUGUI>();

    Debug.Log(scoreText);
  }

  void Start()
  {
  }

  public void UpdateScore(int value)
  {
    scoreText.text = "Score: " + value;
  }

  public void Won(Vector3 _)
  {
    wonObject.SetActive(true);
  }
}
