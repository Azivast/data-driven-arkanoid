using System;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private int numberOfBalls;
    private int numberOfBlocks;


    private void Start() {
        var blocks = Array.FindAll(GameObject.FindGameObjectsWithTag("block"),
            b => b.GetComponent<Block>().type.Destructible);
        numberOfBlocks = blocks.Length;
        Debug.Log(numberOfBalls);
    }

    private void OnEnable() {
        GameplayManager.Events.OnBlockDestroyed += BlockDestroyed;
        GameplayManager.Events.OnBallAmountChange += BallAmountChanged;
        GameplayManager.Events.OnPlayerDeath += ClearPowerups;
    }

    private void OnDisable() {
        GameplayManager.Events.OnBlockDestroyed -= BlockDestroyed;
        GameplayManager.Events.OnBallAmountChange -= BallAmountChanged;
        GameplayManager.Events.OnPlayerDeath -= ClearPowerups;
    }

    private void BlockDestroyed() {
        numberOfBlocks--;
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