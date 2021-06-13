using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static float ChosenBossHealth;
    public static float ChosenBossAttack;
    public static float ChosenBossDef;

    public GameObject bossobj;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bossobj.GetComponent<Health>().totalHP = ChosenBossHealth;
    }
}
