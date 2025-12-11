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

    [SerializeField]
    private AudioClip buttonPushed;

    private void OnEnable() {
      EventManager.coinCollected.Subscribe(this.OnCollectedCoin);
      EventManager.playerWon.Subscribe(this.OnPlayerWon);
      EventManager.buttonPushed.Subscribe(this.OnButtonPushed);
    }

    private void OnDisable() {
      EventManager.coinCollected.Unsubscribe(this.OnCollectedCoin);
      EventManager.playerWon.Unsubscribe(this.OnPlayerWon);
      EventManager.buttonPushed.Unsubscribe(this.OnButtonPushed);
    }

    private void OnCollectedCoin(Vector3 playerPosition) {
      this.PlaySound(this.collectedCoin, playerPosition, 0.75f);
    }

    private void OnPlayerWon(Vector3 playerPosition) {
      this.PlaySound(this.won, playerPosition, 0.75f);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume) {
      var audioSource = Instantiate(this.soundSourcePrefab, position, Quaternion.identity);
      audioSource.clip = audioClip;
      audioSource.volume = volume;

      audioSource.Play();

      var clipLength = audioSource.clip.length;
      Destroy(audioSource.gameObject, clipLength);
    }

    private void OnButtonPushed((int buttonId, Vector3 position) @event) {
      this.PlaySound(this.buttonPushed, @event.position, 1.0f);
    }
  }
}