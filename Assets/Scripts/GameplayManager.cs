using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

[Serializable]
public class GameplayManager : MonoBehaviour {
    [Tooltip("The number of balls the player can lose before the game is over. (Minimum: 1)")] [SerializeField]
    public int playerStartingHealth = 3;
    [SerializeField] private LevelSet levelSet;
    [SerializeField] private string GameOverScene;
    [SerializeField] private string WinScene;
    public static Events Events = new Events();
    
    private int currentLevelID = 0;
    private GameObject currentLevelPrefab;
    [SerializeField]private GameObject LevelComplete;

    private void Start() {
        PlayerStats.Reset(playerStartingHealth);
        Debug.Log($"Starting game with {PlayerStats.Health} player HP.");
        
        currentLevelPrefab = Instantiate(levelSet.Levels[currentLevelID]);
        // Update UI Elements
        Events.PublishHealthChange(0);
        Events.PublishScoreChange(0);
    }
    
    private void OnEnable() {
        Events.OnLevelComplete += LoadNextLevel;
        Events.OnGameOver += LoadGameOver;
    }

    private void OnDisable() {
        Events.OnLevelComplete -= LoadNextLevel;
        Events.OnGameOver -= LoadGameOver;
    }

    private void LoadNextLevel() {
        StartCoroutine(WaitLoadNextLevel());
    }

    private void LoadGameOver() {
        StartCoroutine(WaitGameOver());
    }
    
    IEnumerator WaitLoadNextLevel()
    {
        LevelComplete.SetActive(true);
        Time.timeScale = 0.05f;
        yield return new WaitForSecondsRealtime(4);
        LevelComplete.SetActive(false);
        Time.timeScale = 1;

        Destroy(currentLevelPrefab);
        currentLevelID++;
        if (currentLevelID >= levelSet.Levels.Length) {
            Debug.Log("Loading win screen.");
            SceneManager.LoadScene(WinScene);
        }
        else {
            Debug.Log("Loading next level.");
            currentLevelPrefab = Instantiate(levelSet.Levels[currentLevelID]);
            LevelComplete = GameObject.FindWithTag("levelComplete");
        }
    }
    
    IEnumerator WaitGameOver()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;

        SceneManager.LoadScene(GameOverScene);
    }
}
