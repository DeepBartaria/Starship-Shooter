using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioClip gameAudio;
    private AudioSource gameAudioSource;
    void Start()
    {
        gameAudioSource = gameObject.AddComponent<AudioSource>();
        gameAudioSource.clip = gameAudio;
        gameAudioSource.playOnAwake = true;
        gameAudioSource.loop = true;
        gameAudioSource.volume = 0.1f;
        gameAudioSource.Play();
    }
}
