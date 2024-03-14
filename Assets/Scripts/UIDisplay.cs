using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    Health health;
    Slider slider;
    TextMeshProUGUI scoreText;

    void Awake()
    {
        health = GameObject.Find("Player").GetComponent<Health>();
        // transform.Find does not descend into children of children, that's why we need to use "Panel/HealthSlider" instead of "HealthSlider"
        slider = gameObject.transform.Find("Panel/HealthSlider").gameObject.GetComponent<Slider>();
        scoreText = gameObject.transform.Find("Panel/Score").gameObject.GetComponent<TextMeshProUGUI>();

    }
    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = health.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health.GetHealth();
        scoreText.text = ScoreKeeper.instance.score.ToString("D8");
    }
}
