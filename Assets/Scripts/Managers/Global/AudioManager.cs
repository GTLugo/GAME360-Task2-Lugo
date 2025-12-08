// Based on the implementation by Sasquatch B Studios: https://youtu.be/DU7cgVsU2rM

using UnityEngine;

namespace Managers.Global {
  public class AudioManager : Singleton<AudioManager> {
    [SerializeField]
    private AudioSource soundSourcePrefab;

    [SerializeField]
    private AudioClip collectedCoin;

    [SerializeField]
    private AudioClip won;

    public void CollectedCoin(Vector3 playerPosition) {
      this.PlaySound(this.collectedCoin, playerPosition, 0.75f);
    }

    public void Won(Vector3 playerPosition) {
      this.PlaySound(this.won, playerPosition, 0.75f);
    }

    public void PlaySound(AudioClip audioClip, Vector3 position, float volume) {
      var audioSource = Instantiate(this.soundSourcePrefab, position, Quaternion.identity);
      audioSource.clip = audioClip;
      audioSource.volume = volume;

      audioSource.Play();

      var clipLength = audioSource.clip.length;
      Destroy(audioSource.gameObject, clipLength);
    }
  }
}