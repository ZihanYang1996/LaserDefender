using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.1f;

    [Header("AI")]
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minFiringRate = 0.1f;
    [SerializeField] bool isAI = false;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    [HideInInspector]
    public bool isFiring;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if (isAI)
        {
            isFiring = true;
        }
        else
        {
            isFiring = false;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());  // start the coroutine
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);  // stop the coroutine
            firingCoroutine = null;  // reset the coroutine
        }

    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            if (audioPlayer != null && !isAI)
            {
                audioPlayer.PlayShootingClip();
            }

            GameObject laser = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D laserRigidbody = laser.GetComponent<Rigidbody2D>();
            if (laserRigidbody != null)
            {
                // laserRigidbody.velocity = new Vector2(0, 1) * projectileSpeed;  // move the laser up (in the y direction
                laserRigidbody.velocity = transform.up * projectileSpeed;  // alternative way to move the laser up
            }

            Destroy(laser, projectileLifetime);

            float firingRate = baseFiringRate + Random.Range(-firingRateVariance, firingRateVariance);  // add some randomness to the firing rate (in seconds)
            firingRate = Mathf.Clamp(firingRate, minFiringRate, float.MaxValue);  // make sure the firing rate is not less than the minimum firing rate (in seconds
            yield return new WaitForSeconds(firingRate); // wait for the firing rate (in seconds)
        }
    }
}
