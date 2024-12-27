using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // References to GameObjects
    public TextMeshProUGUI scoreText;        // Changed to TextMeshProUGUI
    public TextMeshProUGUI highScoreText;    // Changed to TextMeshProUGUI

    // Value for above GameObjects
    public float scoreCount;
    public float highScoreCount;

    public float pointsPerSeconds;

    public bool scoreIncreasing;

    public bool shouldDouble;

    // Start is called before the first frame update
    void Start()
    {
        // Optionally initialize high score
        highScoreCount = PlayerPrefs.GetFloat("HighScore", 0); // You can store high score with PlayerPrefs
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSeconds * Time.deltaTime;
        }

        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount); // Save high score if it's the highest
        }

        // Update the UI text elements
        scoreText.text = "Score: " + Mathf.FloorToInt(scoreCount);
        highScoreText.text = "High Score: " + Mathf.FloorToInt(highScoreCount);
    }

    public void AddScore(int pointsToAdd)
    {
        if (shouldDouble)
        {
            pointsToAdd = pointsToAdd * 2;
        }
        scoreCount += pointsToAdd;
    }
}