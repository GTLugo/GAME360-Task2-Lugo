// Based on the implementation by Sasquatch B Studios: https://youtu.be/DU7cgVsU2rM

using UnityEngine;

namespace Game {
  public class AudioManager : MonoBehaviour {
    private static AudioManager s_self;

    [SerializeField] private AudioSource soundSourcePrefab;

    [SerializeField] private AudioClip collectedCoin;

    [SerializeField] private AudioClip won;

    private void Awake() {
      if (!s_self) {
        s_self = this;
      } else {
        Destroy(gameObject);
        Logger.LogError($"Extra {GetType().Name}");
      }
    }

    public void CollectedCoin(Vector3 position) {
      PlaySound(collectedCoin, position, 0.75f);
    }

    public void Won(Vector3 position) {
      PlaySound(won, position, 0.75f);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume) {
      var audioSource = Instantiate(soundSourcePrefab, position, Quaternion.identity);
      audioSource.clip = audioClip;
      audioSource.volume = volume;

      audioSource.Play();

      var clipLength = audioSource.clip.length;
      Destroy(audioSource.gameObject, clipLength);
    }
  }
}