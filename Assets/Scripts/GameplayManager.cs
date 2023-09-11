using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameplayManager : MonoBehaviour {
    public static Events Events = new();

    [Tooltip("The number of balls the player can lose before the game is over. (Minimum: 1)")] [SerializeField]
    public int playerStartingHealth = 3;

    [Tooltip("Set of levels that will be played.")] [SerializeField]
    private LevelSet levelSet;
    [Tooltip("Scene if player looses.")] [SerializeField]
    private string GameOverScene;
    [Tooltip("Scene if player wins.")] [SerializeField]
    private string WinScene;
    [Tooltip("UI element activated once the level has been completed.")] [SerializeField]
    private GameObject LevelComplete;

    private int currentLevelID;
    private GameObject currentLevelPrefab;

    private void Start() {
        PlayerStats.Reset(playerStartingHealth);
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

    private IEnumerator WaitLoadNextLevel() {
        LevelComplete.SetActive(true);
        Time.timeScale = 0.05f;
        yield return new WaitForSecondsRealtime(4);
        LevelComplete.SetActive(false);
        Time.timeScale = 1;

        Destroy(currentLevelPrefab);
        currentLevelID++;
        if (currentLevelID >= levelSet.Levels.Length) {
            SceneManager.LoadScene(WinScene);
        }
        else {
            currentLevelPrefab = Instantiate(levelSet.Levels[currentLevelID]);
            LevelComplete = GameObject.FindWithTag("levelComplete");
        }
    }

    private IEnumerator WaitGameOver() {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;

        SceneManager.LoadScene(GameOverScene);
    }
}