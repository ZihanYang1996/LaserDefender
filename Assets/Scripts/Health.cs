using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] ParticleSystem explosionEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.damage);
            damageDealer.Hit();
        }

    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            PlayHitEffect();
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
}
