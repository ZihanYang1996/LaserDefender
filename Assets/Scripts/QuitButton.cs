using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    Button quitButton;

    void Awake()
    {
        quitButton = gameObject.GetComponent<Button>();
        quitButton.onClick.AddListener(() => LevelManager.instance.QuitGame());
    }
}
