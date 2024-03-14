using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    Button mainMenuButton;
    void Awake()
    {
        mainMenuButton = gameObject.GetComponent<Button>();
        mainMenuButton.onClick.AddListener(() => LevelManager.instance.LoadMainMenu());
    }
}
