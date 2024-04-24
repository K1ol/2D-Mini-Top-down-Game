using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;
    public static int PlayerScore;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    
}
