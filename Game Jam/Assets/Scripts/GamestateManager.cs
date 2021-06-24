using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameStateManager : MonoBehaviour
{
    private class HighScoreEntry
    {
        //we put the score to negative one as it will be an impossible score to reach
        //so we can use it for debugging purposes
        public float playerScore = -1;
        public string playerName = "";
    }
    private class HighScores
    {
        //only keep the top ten players
        public HighScoreEntry[] topPlayers = new HighScoreEntry[10];
    }
    public float playerScore = 0;
    //What do you want the multiple to be per second eg 1 score per second or 1000 per second
    public float scoreTimeMultiplier = 1.0f;
    public bool gameOver = false;
    private string highScorePath = "";
    void Start()
    {
        highScorePath = Application.dataPath + "Highscores.json";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            HighScores scores = ReadHighScore();
            //if (scores == null)
                
            for (int i = 0; i < scores.topPlayers.Length; i++)
            {
                //if its not beating rank 10 no need to cycle
                if (playerScore < scores.topPlayers[i].playerScore && i == 0)
                    break;

                if (playerScore < scores.topPlayers[i].playerScore)
                {

                }
                else if (i == scores.topPlayers.Length)
                {

                }

            }
        }
        else
            playerScore += Time.deltaTime * scoreTimeMultiplier;
    }

    private HighScores ReadHighScore()
    {
        if (File.Exists(highScorePath))
        {
            StreamReader stream = new StreamReader(highScorePath);
            string jsonData = stream.ReadToEnd();
            HighScores scores = JsonUtility.FromJson<HighScores>(jsonData);
            stream.Close();
            return scores;
        }
        return null;
    }

    public void PlayerDead()
    {
        gameOver = true;
    }
}
