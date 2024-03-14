using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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

    public static AudioPlayer instance { get; private set; }

    AudioSource audioSource;

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

        audioSource = gameObject.GetComponent<AudioSource>();
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
            audioSource.Stop();
            audioSource.clip = gameOverMusic;
            audioSource.Play();
        }
    }

    public void PlayMenuMusic()
    {
        if (menuMusic != null)
        {
            audioSource.Stop();
            audioSource.clip = menuMusic;
            audioSource.Play();
        }
    }

    public void PlayGameMusic()
    {
        if (gameMusic != null)
        {
            audioSource.Stop();
            audioSource.clip = gameMusic;
            audioSource.Play();
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
}
