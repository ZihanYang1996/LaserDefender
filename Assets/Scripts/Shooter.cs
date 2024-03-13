using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.1f;

    Coroutine firingCoroutine;

    public bool isFiring;
    void Start()
    {

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
            GameObject laser = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D laserRigidbody = laser.GetComponent<Rigidbody2D>();
            if (laserRigidbody != null)
            {
                laserRigidbody.velocity = new Vector2(0, 1) * projectileSpeed;  // move the laser up (in the y direction
                // laserRigidbody.velocity = transform.up * projectileSpeed;  // alternative way to move the laser up
            }

            Destroy(laser, projectileLifetime);
            yield return new WaitForSeconds(firingRate);  // wait for the firing rate (in seconds
        }
    }
}
