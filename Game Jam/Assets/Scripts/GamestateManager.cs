using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GamestateManager : MonoBehaviour
{
    
    public class HighScore
    {
        public float score = 0;
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
        HighScore scores = ReadHighScore();
        if (playerScore > scores.score)
        {
            scores.score = playerScore;
            StreamWriter stream = new StreamWriter(Application.dataPath + "Highscores.json");
            string data = JsonUtility.ToJson(scores, true);
            stream.Write(data);
            stream.Close();
        }
    }

    public static HighScore ReadHighScore()
    {
        if (File.Exists(Application.dataPath + "Highscores.json"))
        {
            StreamReader stream = new StreamReader(Application.dataPath + "Highscores.json");
            string jsonData = stream.ReadToEnd();
            HighScore scores = JsonUtility.FromJson<HighScore>(jsonData);
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
