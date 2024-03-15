using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 0.5f;

    [Header("Explosion")]
    [SerializeField] AudioClip explosionClip;
    [SerializeField] [Range(0f, 1f)] float explosionVolume = 0.5f;

    [Header("Music")]
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip gameOverMusic;
    [SerializeField] float fadeOutTime = 1f;
    [SerializeField] float fadeInTime = 1f;

    public static AudioPlayer instance { get; private set; }

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
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        float startVolume = audioSource.volume;

        StartCoroutine(FadeOut(audioSource, fadeOutTime));
        StartCoroutine(FadeIn(music, startVolume, fadeInTime));
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {   
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        Destroy(audioSource);
    }

    IEnumerator FadeIn(AudioClip music, float originVolume, float fadeTime)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0;
        audioSource.loop = true;

        audioSource.clip = music;
        audioSource.Play();

        // while (audioSource.volume < originVolume)
        // {
        //     audioSource.volume += originVolume * Time.deltaTime / fadeTime;
        //     yield return null;
        // }

        // Alternative way using Lerp
        float time = 0;
        while (time < fadeTime)
        {
            audioSource.volume = Mathf.Lerp(0, originVolume, time / fadeTime);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = originVolume;  // ensure the volume is set to the original value
    }
}
