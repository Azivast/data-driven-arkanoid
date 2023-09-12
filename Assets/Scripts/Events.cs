using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events {
    public delegate void ValueChangedHandler(int valueChange);
    public delegate void DefaultDelegate();

    public event ValueChangedHandler OnScoreChange;
    public event ValueChangedHandler OnHealthChange;
    public event ValueChangedHandler OnBallAmountChange;
    public event DefaultDelegate OnBlockDestroyed;
    public event DefaultDelegate OnGameOver;
    public event DefaultDelegate OnLevelComplete;
    public event DefaultDelegate OnPlayerDeath;

    // Call from other classes
    public void PublishScoreChange(int scoreChange) {
        PlayerStats.Score += scoreChange;
        OnScoreChange?.Invoke(PlayerStats.Score);
    }

    public void PublishHealthChange(int healthChange) {
        PlayerStats.Health += healthChange;
        OnHealthChange?.Invoke(PlayerStats.Health);
    }
    
    public void PublishBallAmountChange(int ballAmountChange)
        => OnBallAmountChange?.Invoke(ballAmountChange);
    
    public void PublishPlayerDied() {
        OnPlayerDeath?.Invoke();
    }
    
    public void PublishBlockDestroyed() {
        OnBlockDestroyed?.Invoke();
    }
    
    public void PublishGameOver()
        => OnGameOver?.Invoke();
    
    public void PublishLevelComplete()
        => OnLevelComplete?.Invoke();
}
