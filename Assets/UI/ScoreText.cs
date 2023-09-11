using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreText : MonoBehaviour {
    [SerializeField]private TMP_Text text;
    [SerializeField]private string ScorePrefix = "SCORE: ";

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
        text.text = ScorePrefix + newScore.ToString();
    }
}
