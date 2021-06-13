using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public float totalHP;
    public float currentHP;
    public float barFillPercent;
    public float barFillTotal;
    public bool isDead;
    public Image HPBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = totalHP;
        isDead = false;
        barFillPercent = barFillTotal;
    }

    // Update is called once per frame
    void Update()
    {

        /*Health bar test
        if(Input.GetKeyDown(KeyCode.G))
        {
            GainHealth(5);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(5);
        }*/
    }

    public void AdjustHPBar()
    {
       barFillPercent = currentHP / totalHP;

       HPBar.fillAmount = barFillPercent;
    }

    public void GainHealth(float recoveryPoints)
    {
        if((currentHP + recoveryPoints) < totalHP)
        {
            currentHP += recoveryPoints;
        }
        else
        {
            currentHP = totalHP;
        }

        AdjustHPBar();
    }

    public void TakeDamage(float damagePoints)
    {
        if ((currentHP -= damagePoints) <= 0)
        {
            isDead = true;
        }
        else
        {
            currentHP -= damagePoints;
        }

        AdjustHPBar();
    }
}
