using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvas : MonoBehaviour
{
    public Text timeLabel;
    public Text healthLabel;
    public Text scoreLabel;
    public Text highScoreLabel;
    public Text meleeLabel;
    public Text rangedLabel;
    

   

    void Update()
    {
        timeLabel.text = GameManager.instance.timer.ToString("f0");
        healthLabel.text = Hero.instance.GetCurrentHP().ToString("f0");
        scoreLabel.text = GameManager.instance.score.ToString("f0");
        highScoreLabel.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        if (Pistol.instance.GetTimer1() != 0)
        {
            rangedLabel.text = Pistol.instance.GetTimer1().ToString();
            //Debug.Log(Pistol.instance.GetTimer1());
        } else
        {
            rangedLabel.text = "Ready!";
        }
        
        if (Pistol.instance.GetTimer2() != 0)
        {
            meleeLabel.text = Pistol.instance.GetTimer2().ToString();
            //Debug.Log(Pistol.instance.GetTimer2());
        } else
        {
            meleeLabel.text = "Ready!";
        }
    }

   
}
