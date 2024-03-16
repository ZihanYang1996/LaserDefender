using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 0.5f;

    [Header("Explosion")]
    [SerializeField] AudioClip explosionClip;
    [SerializeField][Range(0f, 1f)] float explosionVolume = 0.5f;

    [Header("Music")]
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip gameOverMusic;
    [SerializeField] float fadeOutTime = 1f;
    [SerializeField] float fadeInTime = 1f;
    [SerializeField][Range(0f, 1f)] float musicVolume = 0.5f;

    public static AudioPlayer instance { get; private set; }

    AudioSource audioSourceA;
    AudioSource audioSourceB;

    Dictionary<AudioSource, bool> audioSourceLocks = new Dictionary<AudioSource, bool>();

    bool isPlayingA = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        GameObject subAudioPlayerA = new GameObject("subAudioPlayerA");
        subAudioPlayerA.transform.parent = transform;  // Set the parent of the subAudioPlayerA to the AudioPlayer

        GameObject subAudioPlayerB = new GameObject("subAudioPlayerB");
        subAudioPlayerB.transform.parent = transform;  // Set the parent of the subAudioPlayerB to the AudioPlayer

        // Create two audio sources
        audioSourceA = subAudioPlayerA.AddComponent<AudioSource>();
        audioSourceA.volume = musicVolume;
        audioSourceA.playOnAwake = false;
        audioSourceA.loop = true;

        audioSourceB = subAudioPlayerB.AddComponent<AudioSource>();
        audioSourceB.volume = 0;
        audioSourceB.playOnAwake = false;
        audioSourceB.loop = true;

        audioSourceLocks.Add(audioSourceA, false);
        audioSourceLocks.Add(audioSourceB, false);

    }

    void Update()
    {
        // Adjust the volume of the audio sources
        // if (isPlayingA && audioSourceA.volume != musicVolume)
        // {
        //     audioSourceA.volume = musicVolume;
        // }
        // else if (!isPlayingA && audioSourceB.volume != musicVolume)
        // {
        //     audioSourceB.volume = musicVolume;
        // }
    }

    public void PlayShootingClip()
    {
        if (shootingClip != null)
        {
            PlayClip(shootingClip, shootingVolume);
        }
    }

    public void PlayExplosionClip()
    {
        if (explosionClip != null)
        {
            PlayClip(explosionClip, explosionVolume);
        }
    }

    public void PlayGameOverMusic()
    {
        if (gameOverMusic != null)
        {
            switchMusic(gameOverMusic);
        }
    }

    public void PlayMenuMusic()
    {
        if (menuMusic != null)
        {
            switchMusic(menuMusic);
        }
    }

    public void PlayGameMusic()
    {
        if (gameMusic != null)
        {
            switchMusic(gameMusic);
        }
    }

    void PlayClip(AudioClip audioClip, float volume)
    {
        if (audioClip != null)
        {
            Vector3 position = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }
    }

    private void switchMusic(AudioClip music)
    {
        if (isPlayingA)
        {
            isPlayingA = false;
            StartCoroutine(FadeIn(audioSourceB, music, musicVolume, fadeInTime));
            StartCoroutine(FadeOut(audioSourceA, fadeOutTime));
        }
        else
        {
            isPlayingA = true;
            StartCoroutine(FadeIn(audioSourceA, music, musicVolume, fadeInTime));
            StartCoroutine(FadeOut(audioSourceB, fadeOutTime));
        }
    }

    // Maybe if there are three audio sources, we don't need a lock anymore
    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        while (audioSourceLocks[audioSource])

        {
            yield return null;
        }
        audioSourceLocks[audioSource] = true; // lock the audio source

        float startVolume = audioSource.volume;
        float time = 0;

        while (time < fadeTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, time / fadeTime);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.Stop();
        audioSourceLocks[audioSource] = false;  // unlock the audio source
    }

    IEnumerator FadeIn(AudioSource audioSource, AudioClip music, float targetVolumn, float fadeTime)
    {
        while (audioSourceLocks[audioSource])
        {
            yield return null;
        }
        audioSourceLocks[audioSource] = true; // lock the audio source
        audioSource.clip = music;
        audioSource.Play();

        float time = 0;
        while (time < fadeTime)
        {
            audioSource.volume = Mathf.Lerp(0, targetVolumn, time / fadeTime);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = targetVolumn;  // ensure the volume is set to the original value
        audioSourceLocks[audioSource] = false;  // unlock the audio source
    }
}
