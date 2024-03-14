using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 1f;
    public static LevelManager instance { get; private set; }

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += HandleMusic;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleMusic;
    }

    void HandleMusic(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            AudioPlayer.instance.PlayMenuMusic();
        }
        else if (scene.name == "Game")
        {
            AudioPlayer.instance.PlayGameMusic();
        }
        else if (scene.name == "GameOver")
        {
            AudioPlayer.instance.PlayGameOverMusic();
        }
    }

    public void StartGame()
    {
        ScoreKeeper.instance.ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
