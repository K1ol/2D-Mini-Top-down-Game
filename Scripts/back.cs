using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class back : MonoBehaviour
{
    public Button backButton;
    private bool buttonClicked = false;
    public Text acknowledge;

    void Start()
    {
        backButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        buttonClicked = true;
        acknowledge.gameObject.SetActive(false);
    }

}
