using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnToMenu : MonoBehaviour
{

    public Canvas controls;
    public Canvas menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.esc))
        {
            controls.enabled = false;
            menu.enabled = true;
        }
    }
}
