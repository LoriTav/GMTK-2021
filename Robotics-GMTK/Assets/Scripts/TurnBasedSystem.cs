using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States { PLAYER, BOSS, WON, LOST }

public class TurnBasedSystem : MonoBehaviour
{
    public States state;

    public float playerAttack1;
    public float playerAttack2;
    public float playerAttack3;

    public float bossAttack1;
    public float bossAttack2;
    public float bossAttack3;

    public float potion;

    public GameObject boss;
    public GameObject player;

    private bool bossTurn;

    // Start is called before the first frame update
    void Start()
    {
        state = States.PLAYER;
        bossTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == States.PLAYER)
        {
            //check for player death
            if (player.GetComponent<Health>().currentHP <= 0)
            {
                player.GetComponent<Health>().isDead = true;
                state = States.LOST;
            }

            //check for boss death
            if (boss.GetComponent<Health>().currentHP > 0)
            {
                //take player input for moves and change state
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    boss.GetComponent<Health>().TakeDamage(playerAttack1);
                    state = States.BOSS;
                }

                else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    boss.GetComponent<Health>().TakeDamage(playerAttack2);
                    state = States.BOSS;
                }

                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    boss.GetComponent<Health>().TakeDamage(playerAttack3);
                    state = States.BOSS;
                }

                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    player.GetComponent<Health>().GainHealth(potion);
                    state = States.BOSS;
                }
            }
            else
            {
                boss.GetComponent<Health>().isDead = true;
                state = States.WON;
            }
        }

        if(state == States.BOSS && bossTurn == false)
        {
            bossTurn = true;
            StartCoroutine(BossMove());
        }
    }

    IEnumerator BossMove()
    {
        if(boss.GetComponent<Health>().currentHP <= 0)
        {
            boss.GetComponent<Health>().isDead = true;
            state = States.WON;
        }

        yield return new WaitForSeconds(2f);

        float tempAIMove = Random.Range(1, 5);

        //dont heal on full health so reroll if true
        if (boss.GetComponent<Health>().currentHP >= boss.GetComponent<Health>().totalHP
            && tempAIMove == 4)
        {
            tempAIMove = Random.Range(1, 5);
            
        }
        else
        {
            if(tempAIMove == 1)
            {
                player.GetComponent<Health>().TakeDamage(bossAttack1);
            }
            else if (tempAIMove == 2)
            {
                player.GetComponent<Health>().TakeDamage(bossAttack2);
            }
            else if (tempAIMove == 3)
            {
                player.GetComponent<Health>().TakeDamage(bossAttack3);
            }
            else if(tempAIMove == 4)
            {
                boss.GetComponent<Health>().GainHealth(potion);
            }

            state = States.PLAYER;
            bossTurn = false;
        }

    }
}
