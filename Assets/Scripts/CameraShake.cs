using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeMagnitude = 1.5f;
    [SerializeField] float shakingViolence = 15f;
    [SerializeField] float traumaDecay = 1f;  // How quickly the trauma fades
    [SerializeField] float defaultTraumaIncrease = 0.5f;

    Vector3 initialPosition;
    float perlinSeed;

    bool cameraShakeActive = true;

    public float trauma = 0f;


    
    void Start()
    {
        initialPosition = transform.position;
        perlinSeed = Random.Range(0f, 100f);  // Randomize the seed, set between 0 and 1 for now
    }

    void Update()
    {
        if (cameraShakeActive && trauma > 0f)
        {
            // Shaking duration = trauma / traumaDecay
            Shake();
            trauma -= Time.deltaTime * traumaDecay;
        }
    }

    public void ShakeCamera(float traumaIncrease)
    {
        trauma = Mathf.Clamp01(trauma + traumaIncrease);
    }

    public void ShakeCamera()
    {
        ShakeCamera(defaultTraumaIncrease);
    }

    void Shake()
    {
        transform.position = initialPosition + GenerateNoise() * shakeMagnitude * trauma * trauma;
    }

    Vector3 GenerateNoise()
    {
        // The reason we multiply by 2 and subtract 1 is to get a value between -1 and 1
        return new Vector3(Mathf.PerlinNoise(perlinSeed, Time.time * shakingViolence) * 2 - 1,
                           Mathf.PerlinNoise(perlinSeed + 1, Time.time * shakingViolence) * 2 - 1, 
                           0f);
    }
}
