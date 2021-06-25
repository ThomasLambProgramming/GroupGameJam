using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GamestateManager : MonoBehaviour
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

    public Wall topWall = null;
    public Wall bottomWall = null;
    public Wall leftWall = null;
    public Wall rightWall = null;
    public AudioSource wallMovementAudio;

    public bool testMove = false;
    private bool horizontal = true;

    private Generator gen1 = null;
    private Generator gen2 = null;


    public void FlipWallAxis()
    {
        horizontal = !horizontal;
        //wallMovementAudio.Play();
        topWall.StartMove();
        bottomWall.StartMove();
        leftWall.StartMove();
        rightWall.StartMove();
    }
    //Gonna be honest i didnt know you could => a variable for a return
    public bool isHorizontal() => horizontal;
    public void LightsOff()
    {
        gen1.TurnLightOff();
        gen2.TurnLightOff();
    }
    void Start()
    {
        highScorePath = Application.dataPath + "Highscores.json";
        Generator[] gens = FindObjectsOfType<Generator>();
        gen1 = gens[0];
        gen2 = gens[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (testMove)
        {
            testMove = false;
            FlipWallAxis();
        }
        if (gameOver)
        {
            SceneManager.LoadScene(0);
            ScoreCheck();
        }
        else
            playerScore += Time.deltaTime * scoreTimeMultiplier;
    }

    private void ScoreCheck()
    {
        HighScores scores = ReadHighScore();
        if (scores != null)
        {
            for (int i = 0; i < scores.topPlayers.Length; i++)
            {
                //if the players score is lower than that rank
                if (playerScore < scores.topPlayers[i].playerScore)
                {
                    CyclePlaces(i, scores);
                    break;
                }
                //else the player has the highest score
                else if (i == scores.topPlayers.Length - 1)
                {
                    CyclePlaces(i, scores);
                    break;
                }
            }
        }
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
    private HighScores CyclePlaces(int index, HighScores scores)
    {
        if (index != 0)
        {
            HighScores newScores = new HighScores();

            for (int i = 0; i < newScores.topPlayers.Length; i++)
            {
                //since we are replacing at least one then we can remove the first
                if (i == 0)
                    continue;

                if (i < index)
                {
                    newScores.topPlayers[i - 1] = scores.topPlayers[i];
                }
                else if (i == index)
                {
                    newScores.topPlayers[i].playerScore = playerScore;
                    
                    //add the name part in a ui or something to add to this later
                }
                else if (i > index)
                {
                    newScores.topPlayers[i] = scores.topPlayers[i];
                }
            }
        }

        return scores;
    }

    public void PlayerDead()
    {
        gameOver = true;
    }
}
