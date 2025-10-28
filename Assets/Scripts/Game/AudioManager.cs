// Based on the implementation by Sasquatch B Studios: https://youtu.be/DU7cgVsU2rM

using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager self;

  [SerializeField] private AudioSource soundSource;

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

  public void PlaySound(AudioClip audioClip, Vector3 spawnPoint, float volume)
  {
    AudioSource audioSource = Instantiate(soundSource, spawnPoint, Quaternion.identity);
    audioSource.clip = audioClip;
    audioSource.volume = volume;

    audioSource.Play();

    float clipLength = audioSource.clip.length;
    Destroy(audioSource.gameObject, clipLength);
  }
}
