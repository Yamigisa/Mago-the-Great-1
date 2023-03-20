using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public Text dialogueText;
    public GameObject[] choices;
    
    void Start()
    {
        state = BattleState.START;
        SetUpBattle();
    }

    void SetUpBattle()
    {
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        if (state == BattleState.PLAYERTURN)
        {
            dialogueText.text = "Giliran kamu!";
            PlayerAttack();
        }
        else
            EnemyTurn();
    }
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Menang!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Kalah!";
        }
    }

    IEnumerator PlayerAttack()
    {
        EnemyHUD.instance.EnemyTakeDamage(PlayerHUD.instance.damage);
        yield return new WaitForSeconds(1f);

        if (EnemyHUD.instance.enemylife == 0)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
        StartCoroutine(PlayerAttack());
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = "Giliran musuh!";

        yield return new WaitForSeconds(1f);
        {
            PlayerTurn();
        }
        PlayerHUD.instance.PlayerTakeDamage(EnemyHUD.instance.damage);

        if (PlayerHUD.instance.playerlife == 0)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    public void OnCorrectCard()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerAttack());
    }

    public void OnWrongCard()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(EnemyTurn());
    }
    // Update is called once per frame
    void Update()
    {

    }
}
