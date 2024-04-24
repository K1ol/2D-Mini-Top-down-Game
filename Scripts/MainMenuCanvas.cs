using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{

    public Text acknowled;

    void Awake()
    {
        acknowled.enabled = false;
    }
   
    

    public void Acknowled()
    {
        acknowled.enabled = true;
    }
}
