using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreText : MonoBehaviour {
    [Tooltip("Text object")] [SerializeField]
    private TMP_Text text;
    
    [Tooltip("String that supersedes the actual score number.")] [SerializeField]
    private string scorePrefix = "SCORE: ";

    private void Awake() {
        ScoreChange(PlayerStats.Score);
    }

    private void OnEnable() {
        GameplayManager.Events.OnScoreChange += ScoreChange;
    }
    private void OnDisable() {
        GameplayManager.Events.OnScoreChange -= ScoreChange;
    }

    private void ScoreChange(int newScore) {
        text.text = scorePrefix + newScore.ToString();
    }
}
