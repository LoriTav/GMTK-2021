using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hi : MonoBehaviour {
  
    public Text StartMenu;

    // Start is called before the first frame update
    void Start()
    {
        StartMenu.text = "Title \n press space to begin";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartMenu.text = "You have begun";
        }
    }
}
