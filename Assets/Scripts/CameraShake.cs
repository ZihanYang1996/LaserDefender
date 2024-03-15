using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;

    Vector3 initialPosition;
    float perlinSeed;



    
    void Start()
    {
        initialPosition = transform.position;
        perlinSeed = Random.Range(0f, 1f);  // Randomize the seed, set between 0 and 1 for now
    }

    public void Play()
    {

        StartCoroutine(Shake());

    }


    // IEnumerator Shake(float trauma)
    // {
    //     // isShaking = true;
    //     float elapsedTime = 0f;
    //     while (elapsedTime < shakeDuration)
    //     {
    //         // Shake the camera every frame
    //         transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
    //         elapsedTime += Time.deltaTime;
    //         yield return new WaitForEndOfFrame();
    //     }
    //     isShaking = false;
    // }

    IEnumerator Shake()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            // // The reason we multiply by 2 and subtract 1 is to get a value between -1 and 1
            // Vector3 deltaPosition = new Vector3(Mathf.PerlinNoise(perlinSeed, Time.time) * 2 - 1,
            //                                     Mathf.PerlinNoise(perlinSeed + 1, Time.time) * 2 - 1, 
            //                                     0f);
            // transform.position = initialPosition + deltaPosition * shakeMagnitude;
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
