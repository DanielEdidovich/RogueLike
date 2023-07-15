using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
    public TMP_Text highScoreText; // Ссылка на текстовое поле с текущим рекордом

    void Start()
    {
        int highScoreLevel = HighScoreManager.GetHighScore();
        highScoreText.text = "Текущий рекорд: " + highScoreLevel;
    }
}

