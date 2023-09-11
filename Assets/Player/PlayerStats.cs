using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats {
    private static int score;
    public static int Health;

    public static int Score {
        get => score;
        set => score = value;
    }

    public static void Reset(int health) {
        score = 0;
        Health = health;
    }
}
