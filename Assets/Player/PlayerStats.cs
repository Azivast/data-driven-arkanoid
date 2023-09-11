using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats {
    public static int Score;
    public static int Health;

    public static void Reset(int health) {
        Score = 0;
        Health = health;
    }
}
