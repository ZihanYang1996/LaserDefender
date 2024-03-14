using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] int scoreValue = 10;
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] bool applyCameraShake = true;
    [SerializeField] bool isAI = false;

    CameraShake cameraShake;


    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.damage);
            damageDealer.Hit();
        }

    }

    void TakeDamage(int damage)
    {
        ShakeCamera();
        health -= damage;
        if (health <= 0)
        {
            if (AudioPlayer.instance != null)
            {
                AudioPlayer.instance.PlayExplosionClip();
            }
            PlayHitEffect();
            if (isAI)
            {
                ScoreKeeper.instance.AddScore(scoreValue);
            }
            else
            {
                LevelManager.instance.LoadGameOver();
            }
            Destroy(gameObject);
        }
    }

    void PlayHitEffect()
    {
        if (explosionEffect != null)
        {
            ParticleSystem explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, explosion.main.duration + explosion.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (applyCameraShake && cameraShake != null)
        {
            cameraShake.Play();
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
