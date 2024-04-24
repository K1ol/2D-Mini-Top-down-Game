using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resolutionDropdown : MonoBehaviour
{

    Dropdown dropdown;

    void Awake()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<Dropdown>();
        }
        dropdown.value = 2;
    }

    public void SetResolution()
    {
        switch (dropdown.value)
        {
            case 0: Screen.SetResolution(800, 600, true);break;
            case 1: Screen.SetResolution(1280, 768, true);break;
            case 2: Screen.SetResolution(1920, 1080, true);break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
