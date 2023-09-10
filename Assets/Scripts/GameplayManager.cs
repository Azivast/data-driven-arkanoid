using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameplayManager : MonoBehaviour
{
    [SerializeField] private LevelSet levelSet;
    [SerializeField] private string GameOverScene;
    [SerializeField] private string WinScene;
    public static Events Events = new Events();
    
    private int currentLevelID = 0;
    private GameObject currentLevelPrefab;

    private void Start() {
        currentLevelPrefab = Instantiate(levelSet.Levels[currentLevelID]);
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
        
        yield return new WaitForSeconds(4);

        Destroy(currentLevelPrefab);
        currentLevelID++;
        if (currentLevelID >= levelSet.Levels.Length) {
            SceneManager.LoadScene(WinScene);
        }
        else currentLevelPrefab = Instantiate(levelSet.Levels[currentLevelID]);
    }
    
    IEnumerator WaitGameOver()
    {
        
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(GameOverScene);
    }
}
