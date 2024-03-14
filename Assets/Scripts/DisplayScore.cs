using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    void Start()
    {
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score:\n" + ScoreKeeper.instance.score.ToString("D8");
    }
}
