using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class controls : MonoBehaviour
{
    public Canvas Start;
    public Canvas Controls;
    void Start()
    {
       // Controls.text = "Press A to view controls";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Controls.text = "Viewing Controls";
            Start.enabled = false;
            Controls.enabled = true;
        }
    }
}
