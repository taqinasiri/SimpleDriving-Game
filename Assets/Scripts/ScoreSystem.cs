using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier = 1;

    public const string HighScoreKey = "HighScore";

    private float score;

    private void Update()
    {
        score += Time.deltaTime * scoreMultiplier;

        scoreText.text = Mathf.FloorToInt(score).ToString("000");
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey,0);
        if(score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey,Mathf.FloorToInt(score));
        }
    }
}