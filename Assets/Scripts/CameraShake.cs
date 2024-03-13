using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;

    Vector3 initialPosition;

    bool isShaking = false;  // flag to check if the camera is shaking
    
    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {
        if (!isShaking)
        {
            // To avoid starting the coroutine multiple times
            StartCoroutine(Shake());
        }
    }
    IEnumerator Shake()
    {
        isShaking = true;
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            // Shake the camera every frame
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        isShaking = false;
    }
}
