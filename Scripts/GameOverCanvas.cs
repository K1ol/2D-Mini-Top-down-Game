using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    public Text scoreLabel;


    
    void Update()
    {
        scoreLabel.text = GameManager.instance.score.ToString();
    }
}
