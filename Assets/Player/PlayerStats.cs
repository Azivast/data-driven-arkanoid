public static class PlayerStats {
    public static int Health;

    public static int Score { get; set; }

    public static void Reset(int health) {
        Score = 0;
        Health = health;
    }
}