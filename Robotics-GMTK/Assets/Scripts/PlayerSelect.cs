using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    public Image Selector;
    public Transform place1;
    public Transform place2;
    public Transform place3;
    public Transform currentPlace;
    // Start is called before the first frame update
    void Start()
    {
        currentPlace = place2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentPlace == place2)
            {
                Selector.transform.position = place3.position;
                currentPlace = place3;
            }
            else if (currentPlace == place3)
            {
                Selector.transform.position = place1.position;
                currentPlace = place1;
            }
            else
            {
                Selector.transform.position = place2.position;
                currentPlace = place2;
            }
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentPlace == place2)
            {
                Selector.transform.position = place1.position;
                currentPlace = place1;
            }
            else if (currentPlace == place3)
            {
                Selector.transform.position = place2.position;
                currentPlace = place2;
            }
            else
            {
                Selector.transform.position = place3.position;
                currentPlace = place3;
            }
        }
    }
}
