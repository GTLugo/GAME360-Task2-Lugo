using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager self;

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
  }

    // Update is called once per frame
    void Update()
    {
        
    }
}
