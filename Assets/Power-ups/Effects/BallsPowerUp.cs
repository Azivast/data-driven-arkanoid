using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsPowerUp : MonoBehaviour {
    [Tooltip("The amount of balls to spawn on each ball.")]
    public int SpawnAmount = 1;

    void OnValidate() {
        if (SpawnAmount < 1) {
            Debug.LogWarning("Cannot spawn less than 1 ball");
            SpawnAmount = 1;
        }
    }

    void OnEnable() {
        foreach (GameObject ballObject in GameObject.FindGameObjectsWithTag("ball")) {
            Ball ball = ballObject.GetComponent<Ball>();
            for (int i = 0; i < SpawnAmount; i++) {
                var newBall = Instantiate(ball, GameObject.FindWithTag("level").transform);
                float speed = ball.Velocity.magnitude;
                Vector2 randomDirection = Random.insideUnitCircle;
                newBall.Velocity = randomDirection * speed;
            }
        }
    }
}