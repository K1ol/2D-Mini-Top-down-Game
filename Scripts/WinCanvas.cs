using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCanvas : MonoBehaviour
{
    public Text highScoreLabel;
    public Text scoreLabel;
 

    // Update is called once per frame
    void Update()
    {
        highScoreLabel.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        scoreLabel.text = GameManager.instance.score.ToString();
    }
}
