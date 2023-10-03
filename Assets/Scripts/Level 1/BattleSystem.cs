using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST, BLANK, NOTHING, BLOCK, PLAYATTACK, PLAYBLOCK}
public class BattleSystem : MonoBehaviour
{
    public static BattleSystem instance;
    public BattleState state;
    public int round;
    public bool hasPlayerAttacked = false;
    public bool hasPlayerDefended = false;
    public bool hasEnemyAttacked = false;
    public bool hasAttackPlayed = false;
    public bool hasDefendPlayed = false;
    private ThisCard thisCard;
    public TMP_Text enemynametext;
    public TMP_Text formulatext;
    public TMP_Text enemylifetext;
    public GameObject WinScreen;
    public GameObject LoseScreen;
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
    public GameObject[] cardround5;
    public List<GameObject> _currentCard;
    [SerializeField] private List<Transform> _cardPost;
    [SerializeField] private int _selectedCardPower;
    [SerializeField] private List<Transform> dialogueposition;
    public GameObject[] dialogueicon;
    public List<GameObject> _dialogueselectionbox;

    void Start()
    {
        round = 0;
        state = BattleState.PLAYERTURN;
        enemynametext.text = "Netsu";
        StartCoroutine(SwordIcon());
    }

    IEnumerator SwordIcon()
    {
        if (state == BattleState.PLAYERTURN)
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
                    formulatext.text = "+3";
                    enemylifetext.text = "5";
                    enemy_life = 5;
                    formula = 3;
                    TurnLeft = 5;
                    break;
                case 2:
                    formulatext.text = "-4";
                    enemylifetext.text = "3";
                    enemy_life = 3;
                    formula = 4;
                    TurnLeft = 4;
                    Destroy(TurnObject[TurnLeft].gameObject);
                    break;
                case 3:
                    formulatext.text = "+2";
                    enemylifetext.text = "8";
                    enemy_life = 8;
                    formula = 2;
                    TurnLeft = 3;
                    Destroy(TurnObject[TurnLeft].gameObject);
                    break;
                case 4:
                    formulatext.text = "-5";
                    enemylifetext.text = "3";
                    enemy_life = 3;
                    formula = 5;
                    TurnLeft = 2;
                    Destroy(TurnObject[TurnLeft].gameObject);
                    break;
                case 5:
                    formulatext.text = "+5";
                    enemylifetext.text = "8";
                    enemy_life = 5;
                    formula = 8;
                    TurnLeft = 1;
                    Destroy(TurnObject[TurnLeft].gameObject);
                    break;
                case 6:
                    TurnLeft = 0;
                    state = BattleState.LOST;
                    EndBattle();
                    break;
                default:
                    break;
            }
            summonCard();
            state = BattleState.NOTHING;
            _dialogueselectionbox.Add(Instantiate(dialogueicon[0], dialogueposition[0].position, Quaternion.identity));
            _dialogueselectionbox[0].transform.SetParent(dialogueposition[0]);
            yield return new WaitForSeconds(2.5f);
            Destroy(_dialogueselectionbox[0]);
            _dialogueselectionbox.RemoveAt(0);
            _dialogueselectionbox.Add(Instantiate(dialogueicon[2], dialogueposition[1].position, Quaternion.identity));
            _dialogueselectionbox[0].transform.SetParent(dialogueposition[0]);   
            state = BattleState.PLAYERTURN;
        }
    }
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            WinScreen.SetActive(true);
        }
        else if (state == BattleState.LOST)
        {
            LoseScreen.SetActive(true);
        }
    }

    IEnumerator PlayerAttack()
    {

        if (state == BattleState.PLAYERTURN)
        {
            if (hasPlayerAttacked == false)
            {
                hasPlayerAttacked = true;
                state = BattleState.BLANK;
                enemyhudanimator.SetTrigger("Enemy_Fill_Attacked");
                enemyanimator.SetTrigger("Seigi_Attacked");
                yield return new WaitForSeconds(2f);
                EnemyHUD.instance.EnemyTakeDamage(PlayerHUD.instance.damage);
            }
        }
        if (TurnLeft == 0)
        {
            Destroy(_dialogueselectionbox[0]);
            _dialogueselectionbox.RemoveAt(0);
            state = BattleState.LOST;
            EndBattle();
        }
        if (EnemyHUD.instance.enemylife == 0)
        {
            state = BattleState.WON;
            Destroy(_dialogueselectionbox[0]);
            _dialogueselectionbox.RemoveAt(0);
            EndBattle();
        }
        else
        {
            StartCoroutine(ShieldIcon());
        }
    }
    IEnumerator EnemyTurn()
    {
        if (state == BattleState.ENEMYTURN)
        {
            if(!hasEnemyAttacked)
            {
                state = BattleState.NOTHING;
                hasEnemyAttacked = true;
                playeranimator.SetTrigger("Mago_Attacked");
                playerhudanimator.SetTrigger("Fill_Attacked");
                enemyanimator.SetTrigger("Seigi_Attack");
                yield return new WaitForSeconds(2f);
                PlayerHUD.instance.PlayerTakeDamage(EnemyHUD.instance.damage);
            }
        }
        if (PlayerHUD.instance.playerlife == 0)
        {
            state = BattleState.LOST;
            Destroy(_dialogueselectionbox[0]);
            _dialogueselectionbox.RemoveAt(0);
            EndBattle();
        }
        else
        { 
            for (int i = 0; i < _cardPost.Count; i++)
            {
                Destroy(_currentCard[0]);
                _currentCard.RemoveAt(0);
            }
            Destroy(_dialogueselectionbox[0]);
            _dialogueselectionbox.RemoveAt(0);
            state = BattleState.PLAYERTURN;
            StartCoroutine(SwordIcon());
        }
    }

    public void AttackAnswer()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        if (TurnLeft % 2 != 0)
        {
            if (_selectedCardPower + formula == enemy_life)
            {
                state = BattleState.PLAYERTURN;
                StartCoroutine(PlayerAttack());
            }
            else
            {
                state = BattleState.PLAYATTACK;
                enemyanimator.SetTrigger("Seigi_Miss");
                StartCoroutine(ShieldIcon());
            }
        }
        else if (TurnLeft % 2 == 0)
        {
            if (_selectedCardPower - formula == enemy_life)
            {
                state = BattleState.PLAYERTURN;
                StartCoroutine(PlayerAttack());
            }
            else
            {
                state = BattleState.PLAYATTACK;
                enemyanimator.SetTrigger("Seigi_Miss");
                StartCoroutine(ShieldIcon());
            }
        }
    }

    IEnumerator ShieldIcon()
    {
        if(state != BattleState.BLANK)
        {
            yield return new WaitForSeconds(2f);    
            state = BattleState.NOTHING;
        }
        Destroy(_dialogueselectionbox[0]);
        _dialogueselectionbox.RemoveAt(0);
        _dialogueselectionbox.Add(Instantiate(dialogueicon[1], dialogueposition[0].position, Quaternion.identity));
        _dialogueselectionbox[0].transform.SetParent(dialogueposition[0]);
        yield return new WaitForSeconds(2.5f);
        Destroy(_dialogueselectionbox[0]);
        _dialogueselectionbox.RemoveAt(0);
        _dialogueselectionbox.Add(Instantiate(dialogueicon[3], dialogueposition[1].position, Quaternion.identity));
        _dialogueselectionbox[0].transform.SetParent(dialogueposition[0]);
        BlockTurn();
    }
    void BlockTurn()
    {
        state = BattleState.BLOCK;
        switch (round)
        {
            case 1:
                enemylifetext.text = "4";
                enemy_life = 4;
                break;
            case 2:
                enemylifetext.text = "4";
                enemy_life = 4;
                break;
            case 3:
                enemylifetext.text = "5";
                enemy_life = 5;
                break;
            case 4:
                enemylifetext.text = "1";
                enemy_life = 1;
                break;
            case 5:
                enemylifetext.text = "8";
                enemy_life = 8;
                break;
            default:
                break;
        }
    }
    public void DefendAnswer()
    {
        if (state != BattleState.BLOCK)
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
                state = BattleState.ENEMYTURN;
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
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator WaitForSwordIcon()
    {
        state = BattleState.PLAYBLOCK;
        enemyanimator.SetTrigger("Seigi_Attack");
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < _cardPost.Count; i++)
            {
                Destroy(_currentCard[0]);
                _currentCard.RemoveAt(0);
            }
        Destroy(_dialogueselectionbox[0]);
        _dialogueselectionbox.RemoveAt(0);
        state = BattleState.PLAYERTURN;
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
                        card.GetComponent<ThisCard>().setAnimator(playeranimator);
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
                            card.GetComponent<ThisCard>().setAnimator(playeranimator);
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
                            card.GetComponent<ThisCard>().setAnimator(playeranimator);
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
                            card.GetComponent<ThisCard>().setAnimator(playeranimator);
                            _currentCard.Add(card);
                            _currentCard[i].transform.SetParent(_cardPost[i]);
                        }
                    }
                    break;

                case 5:
                    {
                        for (int i = 0; i < _cardPost.Count; i++)
                        {
                            GameObject card = Instantiate(cardround5[i], _cardPost[i].position, Quaternion.identity);
                            card.GetComponent<ThisCard>().setAnimator(playeranimator);
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