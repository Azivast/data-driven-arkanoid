using System;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private int numberOfBalls;
    private int numberOfBlocks;

    private void OnEnable() {
        GameplayManager.Events.OnBlockDestroyed += BlockDestroyed;
        GameplayManager.Events.OnBallAmountChange += BallAmountChanged;
        GameplayManager.Events.OnPlayerDeath += ClearPowerups;
        
        var blocks = Array.FindAll(GameObject.FindGameObjectsWithTag("block"),
            b => b.GetComponent<Block>().type.Destructible);
        numberOfBlocks = blocks.Length;
        Debug.Log($"Starting new level, found {numberOfBlocks} blocks.");
    }

    private void OnDisable() {
        GameplayManager.Events.OnBlockDestroyed -= BlockDestroyed;
        GameplayManager.Events.OnBallAmountChange -= BallAmountChanged;
        GameplayManager.Events.OnPlayerDeath -= ClearPowerups;
    }

    private void BlockDestroyed() {
        numberOfBlocks--;
        Debug.Log($"{numberOfBlocks} blocks left.");
        if (numberOfBlocks <= 0) GameplayManager.Events.PublishLevelComplete();
    }

    private void BallAmountChanged(int change) {
        numberOfBalls += change;
        if (numberOfBalls <= 0) GameplayManager.Events.PublishPlayerDied();
    }

    private void ClearPowerups() {
        var powerups = GameObject.FindGameObjectsWithTag("powerupCapsule");
        foreach (var capsule in powerups) Destroy(capsule);
    }
}