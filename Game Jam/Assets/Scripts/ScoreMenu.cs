using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    public GameObject ScoreText;
    public Text scoreTextt;
    private void Start()
    {
        var score = GamestateManager.ReadHighScore();
        scoreTextt = ScoreText.GetComponent<Text>();
        scoreTextt.text = "HIGH SCORE: " + Mathf.CeilToInt(score.score).ToString();
    }
}
