// Based on the implementation by Sasquatch B Studios: https://youtu.be/DU7cgVsU2rM

using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager self;

  [SerializeField] private AudioSource soundSource;
  [SerializeField] private AudioClip collectedCoin;
  [SerializeField] private AudioClip won;

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

  public void CollectedCoin(Vector3 position)
  {
    PlaySound(collectedCoin, position, 0.75f);
  }
  
  public void Won(Vector3 position)
  {
    PlaySound(won, position, 0.75f);
  }

  void PlaySound(AudioClip audioClip, Vector3 position, float volume)
  {
    AudioSource audioSource = Instantiate(soundSource, position, Quaternion.identity);
    audioSource.clip = audioClip;
    audioSource.volume = volume;

    audioSource.Play();

    float clipLength = audioSource.clip.length;
    Destroy(audioSource.gameObject, clipLength);
  }
}
