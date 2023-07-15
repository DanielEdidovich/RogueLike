using UnityEngine;

public static class HighScoreManager
{
    private static string highScoreKey = "HighScore"; // Ключ для сохранения рекорда в PlayerPrefs

    public static void SetHighScore(int level)
    {
        int currentHighScore = PlayerPrefs.GetInt(highScoreKey, 0);
        if (level > currentHighScore)
        {
            PlayerPrefs.SetInt(highScoreKey, level);
        }
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(highScoreKey, 0);
    }
}

