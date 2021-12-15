using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddHighScoreValue : MonoBehaviour
{
    [SerializeField] Text highScore;
    private void Start()
    {
        GetHighScore();
    }
    public void GetHighScore()
    {
        if(HttpCookie.GetCookie("Score") != "")
        highScore.text = "High score : " +  HttpCookie.GetCookie("Score");
    }
}
