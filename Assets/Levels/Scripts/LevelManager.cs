using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private int numberOfBlocks;
    private int numberOfBalls = 1;
    void Start()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");
        numberOfBlocks = blocks.Length;
        
        GameplayManager.Events.OnBlockDestroyed += BlockDestroyed;
        GameplayManager.Events.OnBallAmountChange += BallAmountChanged;
        GameplayManager.Events.OnPlayerDeath += ClearPowerups;
    }

    private void BlockDestroyed() {
        numberOfBlocks--;

        if (numberOfBlocks <= 0) {
            GameplayManager.Events.PublishLevelComplete();
        }
    }
    
    private void BallAmountChanged(int change) {
        numberOfBalls += change;

        if (numberOfBalls <= 0) {
            GameplayManager.Events.PublishPlayerDied();
        }
    }

    private void ClearPowerups() {
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("powerupCapsule");
        foreach (GameObject capsule in powerups) {
            Destroy(capsule);
        }
    }
    
    private void OnDisable() {
        GameplayManager.Events.OnBlockDestroyed -= BlockDestroyed;
    }
}
