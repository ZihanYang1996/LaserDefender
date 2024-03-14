using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    Button playButton;

    void Awake()
    {
        playButton = gameObject.GetComponent<Button>();
        playButton.onClick.AddListener(() => LevelManager.instance.StartGame());
    }
}
