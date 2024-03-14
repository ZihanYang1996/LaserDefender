using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance { get; private set;}

    private int _score = 0;
    public int score { get {return _score;} }

    void Awake()
    {
        if (instance == null)
        {
            // if the instance is null, this is the first instance
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // if the instance is not null, and it is not this instance, destroy this instance
            Destroy(gameObject);
        }
    }
    
    public void AddScore(int score)
    {
        _score += score;
        Mathf.Clamp(_score, 0, int.MaxValue);
        // Debug.Log("Score: " + _score);
    }

    public void ResetScore()
    {
        _score = 0;
    }
}
