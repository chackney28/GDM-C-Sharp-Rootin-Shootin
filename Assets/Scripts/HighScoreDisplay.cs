using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HighScoresDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;
    
    void Start()
    {
        DisplayHighScores();
    }
    
    void DisplayHighScores()
    {
        List<HighScore> topScores = DatabaseManager.Instance.GetTopHighScores(5);
        
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < topScores.Count)
            {
                HighScore score = topScores[i];
                int rank = i + 1;
                scoreTexts[i].text = rank + ". " + score.Type + " - " + 
                                    score.Wave + " Waves";
            }
            else
            {
                scoreTexts[i].text = (i + 1) + ". ---";
            }
        }
    }
}