using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
    public TMP_Text highScoreText; // ������ �� ��������� ���� � ������� ��������

    void Start()
    {
        int highScoreLevel = HighScoreManager.GetHighScore();
        highScoreText.text = "������� ������: " + highScoreLevel;
    }
}

