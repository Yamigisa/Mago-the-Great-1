using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum TutorialBattleState {START, PLAYERTURN, ENEMYTURN, WON, BLANK, NOTHING, BLOCK, PLAYATTACK, PLAYBLOCK}
public class TutorialBattleSystem : MonoBehaviour
{
    public static TutorialBattleSystem instance;
    public TutorialBattleState state;
    // public Round round;
    public GameObject Dialogue16;
    public bool isdialogue16done = false;
    public GameObject Dialogue18;
    public GameObject Dialogue24;
    public GameObject Dialogue28;
    public GameObject TutorialBackground;
    public int round;
    public bool hasPlayerAttacked = false;
    public bool hasPlayerDefended = false;
    public bool hasEnemyAttacked = false;
    public bool hasAttackPlayed = false;
    public bool hasDefendPlayed = false;
    public TMP_Text enemynametext;
    public GameObject TutorialMenu;
    public GameObject WinScreen;
    public TMP_Text formulatext;
    public TMP_Text enemylifetext;
    public Animator playeranimator;
    public Animator enemyanimator;
    public Animator playerhudanimator;
    public Animator enemyhudanimator;
    public int enemy_life;
    public int formula;
    public int TurnLeft;
    [SerializeField] GameObject[] TurnObject;
    public GameObject[] cardround1;
    public GameObject[] cardround2;
    public GameObject[] cardround3;
    public GameObject[] cardround4;
    public List<GameObject> _currentCard;
    [SerializeField] private List<Transform> _cardPost;
    [SerializeField] private int _selectedCardPower;
    [SerializeField] private List<Transform> dialogueposition;
    public GameObject[] dialogueicon;
    public List<GameObject> _dialogueselectionbox;
     void Start()
    {
        round = 0;
        state = TutorialBattleState.PLAYERTURN;
        enemynametext.text = "Congli";
        StartCoroutine(SwordIcon());
    }
    IEnumerator SwordIcon()
    {
        if (state == TutorialBattleState.PLAYERTURN)
        {
            if(hasPlayerAttacked == true || hasAttackPlayed == true || hasPlayerDefended == true || hasDefendPlayed == true || hasAttackPlayed == true)
            {
                hasPlayerAttacked = false;
                hasEnemyAttacked = false;
                hasPlayerDefended = false;
                hasDefendPlayed = false;
                hasAttackPlayed = false;
            }
            round++;
            switch (round)
            {
                case 1:
                    formulatext.text = "+2";
                    enemylifetext.text = "4";
                    enemy_life = 4;
                    formula = 2;
                    TurnLeft = 5;
                    break;
                case 2:
                isdialogue16done = true;
                TutorialBackground.SetActive(true);
                Dialogue18.SetActive(true);
                    formulatext.text = "-99";
                    enemylifetext.text = "99";
                    enemy_life = 99;
                    formula = 99;
                    TurnLeft = 4;
                    Destroy(TurnObject[TurnLeft].gameObject);
                    break;
                case 3:
                Dialogue28.SetActive(true);
            TutorialBackground.SetActive(true);
                    formulatext.text = "+3";
                    enemylifetext.text = "7";
                    enemy_life = 7;
                    formula = 3;
                    TurnLeft = 3;
                    Destroy(TurnObject[TurnLeft].gameObject);
                    break;
                case 4:
                GameObject SwordIcon = GameObject.Find("SmallIcon 1");
            SwordIcon.SetActive(false);
            GameObject ShieldIcon = GameObject.Find("SmallIcon 2");
            SwordIcon.SetActive(false);
                    formulatext.text = "-1";
                    enemylifetext.text = "1";
                    enemy_life = 1;
                    formula = 1;
                    TurnLeft = 2;
                    break;
                default:
                break;
            }
            summonCard();
            state = TutorialBattleState.NOTHING;
            if(round <= 3)
            {
            yield return new WaitForSeconds(2.5f);
            }
            if(round == 4)
            {
                _dialogueselectionbox.Add(Instantiate(dialogueicon[0], dialogueposition[0].position, Quaternion.identity));
            _dialogueselectionbox[0].transform.SetParent(dialogueposition[0]);
            yield return new WaitForSeconds(2.5f);
            Destroy(_dialogueselectionbox[0]);
            _dialogueselectionbox.RemoveAt(0);
            _dialogueselectionbox.Add(Instantiate(dialogueicon[2], dialogueposition[1].position, Quaternion.identity));
            _dialogueselectionbox[0].transform.SetParent(dialogueposition[0]);
            }
            state = TutorialBattleState.PLAYERTURN;
        }
    }

    void EndBattle()
    {
        if (state == TutorialBattleState.WON)
        {
            WinScreen.SetActive(true);
        }
    }

    IEnumerator PlayerAttack()
    {
        if (state == TutorialBattleState.PLAYERTURN)
        {
            if (hasPlayerAttacked == false)
            {
                hasPlayerAttacked = true;
                state = TutorialBattleState.BLANK;
                enemyhudanimator.SetTrigger("Enemy_Fill_Attacked");
                enemyanimator.SetTrigger("Congli_Attacked");
                yield return new WaitForSeconds(2f);
                EnemyHUD.instance.EnemyTakeDamage(PlayerHUD.instance.damage);
            }
        }
        if (EnemyHUD.instance.enemylife == 0)
        {
             Destroy(_dialogueselectionbox[0]);
            _dialogueselectionbox.RemoveAt(0);
            state = TutorialBattleState.WON;
            EndBattle();
        }
        else
        {
            StartCoroutine(ShieldIcon());
        }
    }

    IEnumerator EnemyTurn()
    {
        if (state == TutorialBattleState.ENEMYTURN)
        {
            if(!hasEnemyAttacked)
            {
                state = TutorialBattleState.NOTHING;
                hasEnemyAttacked = true;
                playeranimator.SetTrigger("Mago_Attacked");
                playerhudanimator.SetTrigger("Fill_Attacked");
                enemyanimator.SetTrigger("Congli_Attack");
                yield return new WaitForSeconds(2f);
                PlayerHUD.instance.PlayerTakeDamage(EnemyHUD.instance.damage);
            }
        }
        for (int i = 0; i < _cardPost.Count; i++)
        {
            Destroy(_currentCard[0]);
            _currentCard.RemoveAt(0);
        }
        state = TutorialBattleState.PLAYERTURN;
        StartCoroutine(SwordIcon());
    }

    public void AttackAnswer()
    {
        if (state != TutorialBattleState.PLAYERTURN)
            return;
        if (TurnLeft % 2 != 0)
        {
            if (_selectedCardPower + formula == enemy_life)
            {
                state = TutorialBattleState.PLAYERTURN;
                StartCoroutine(PlayerAttack());
            }
            else
            {
                state = TutorialBattleState.PLAYATTACK;
                enemyanimator.SetTrigger("Congli_Miss");
                StartCoroutine(ShieldIcon());
            }
        }
        else if (TurnLeft % 2 == 0)
        {
            if (_selectedCardPower - formula == enemy_life)
            {
                state = TutorialBattleState.PLAYERTURN;
                StartCoroutine(PlayerAttack());
            }
            else
            {
                state = TutorialBattleState.PLAYATTACK;
                enemyanimator.SetTrigger("Congli_Miss");
                StartCoroutine(ShieldIcon());
            }
        }
    }

    IEnumerator ShieldIcon()
    {
        if (EnemyHUD.instance.enemylife == 2 && isdialogue16done == false)
        {
            TutorialBackground.SetActive(true);
            Dialogue16.SetActive(true);
            GameObject SwordIcon = GameObject.Find("SmallIcon 1");
            SwordIcon.SetActive(false);
        }
        if (EnemyHUD.instance.enemylife == 2 && isdialogue16done == true)
                {
                    TutorialBackground.SetActive(true);
                    Dialogue24.SetActive(true);
                }
        if(state != TutorialBattleState.BLANK)
        {
            yield return new WaitForSeconds(2f);    
            state = TutorialBattleState.NOTHING;
        }
        if(round >= 3)
        {
        _dialogueselectionbox.Add(Instantiate(dialogueicon[1], dialogueposition[0].position, Quaternion.identity));
        _dialogueselectionbox[0].transform.SetParent(dialogueposition[0]);
        yield return new WaitForSeconds(2.5f);
        Destroy(_dialogueselectionbox[0]);
        _dialogueselectionbox.RemoveAt(0);
        _dialogueselectionbox.Add(Instantiate(dialogueicon[3], dialogueposition[1].position, Quaternion.identity));
        _dialogueselectionbox[0].transform.SetParent(dialogueposition[0]);
        }
        BlockTurn();
    }

    void BlockTurn()
    {
        state = TutorialBattleState.BLOCK;
        switch (round)
        {
            case 1:
            formulatext.text = "+4";
            formula = 4;
            enemylifetext.text = "7";
            enemy_life = 7;
                break;
            case 2:
                enemylifetext.text = "99";
                enemy_life = 99;
                 hasDefendPlayed = true;
                break;
            case 3:
                enemylifetext.text = "9";
                enemy_life = 9;
                break;
            case 4:
                enemylifetext.text = "4";
                enemy_life = 4;
                break;
            default:
                break;
        }
    }

    public void DefendAnswer()
    {
        if (state != TutorialBattleState.BLOCK)
            return;
        if (TurnLeft % 2 != 0)
        {
            if (_selectedCardPower + formula == enemy_life)
            {
                if(!hasPlayerDefended)
                {
                    hasPlayerDefended = true;
                    StartCoroutine(WaitForSwordIcon());
                }
            }
            else
            {
                state = TutorialBattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else if (TurnLeft % 2 == 0)
        {
            if (_selectedCardPower - formula == enemy_life)
            {
                if(!hasPlayerDefended)
                {
                    hasPlayerDefended = true;
                    StartCoroutine(WaitForSwordIcon());
                }
            }
            else
            {
                state = TutorialBattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator WaitForSwordIcon()
    {
        state = TutorialBattleState.PLAYBLOCK;
        enemyanimator.SetTrigger("Congli_Attack");
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < _cardPost.Count; i++)
            {
                Destroy(_currentCard[0]);
                _currentCard.RemoveAt(0);
            }
        if(round >= 3)
        {
            Destroy(_dialogueselectionbox[0]);
            _dialogueselectionbox.RemoveAt(0);
        }
        state = TutorialBattleState.PLAYERTURN;
        StartCoroutine(SwordIcon());
    }

     private void summonCard()
     {
        if (_currentCard.Count == 0)
        {
            switch (round)
            {
                case 1:
                    {
                        for (int i = 0; i < _cardPost.Count; i++)
                        {
                        GameObject card = Instantiate(cardround1[i], _cardPost[i].position, Quaternion.identity);
                        card.GetComponent<TutorialThisCard>().setAnimator(playeranimator);
                        _currentCard.Add(card);
                        _currentCard[i].transform.SetParent(_cardPost[i]);
                        }
                    }
                    break;

                case 2:
                    {
                        for (int i = 0; i < _cardPost.Count; i++)
                        {
                            GameObject card = Instantiate(cardround2[i], _cardPost[i].position, Quaternion.identity);
                            card.GetComponent<TutorialThisCard>().setAnimator(playeranimator);
                            _currentCard.Add(card);
                            _currentCard[i].transform.SetParent(_cardPost[i]);
                        }
                    }
                    break;

                case 3:
                    {
                        for (int i = 0; i < _cardPost.Count; i++)
                        {
                            GameObject card = Instantiate(cardround3[i], _cardPost[i].position, Quaternion.identity);
                            card.GetComponent<TutorialThisCard>().setAnimator(playeranimator);
                            _currentCard.Add(card);
                            _currentCard[i].transform.SetParent(_cardPost[i]);
                        }
                    }
                    break;

                case 4:
                    {
                        for (int i = 0; i < _cardPost.Count; i++)
                        {
                            GameObject card = Instantiate(cardround4[i], _cardPost[i].position, Quaternion.identity);
                            card.GetComponent<TutorialThisCard>().setAnimator(playeranimator);
                            _currentCard.Add(card);
                            _currentCard[i].transform.SetParent(_cardPost[i]);
                        }
                    }
                    break;
                default:
                    break;


            }
        }
    }

    public void getSelectedCardPower(int Power)
    {
        _selectedCardPower = Power;
    }

    private void Awake()
    {
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }
}