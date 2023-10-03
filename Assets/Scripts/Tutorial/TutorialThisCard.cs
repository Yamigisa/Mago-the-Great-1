using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TutorialThisCard : MonoBehaviour
{
    public Card cardSO;
    public int id;
    public int power;
    public Animator _animator;
    [SerializeField] private AudioSource CardAttackSFX;
    [SerializeField] private AudioSource CardDefendSFX;

    private void Awake()
    {
        id = cardSO.id;
        power = cardSO.power;
    }
    public void setAnimator(Animator animator)
    {
        _animator = animator;
    }
    public void AttackAnimation()
    {
        if(TutorialBattleSystem.instance.state == TutorialBattleState.BLANK || TutorialBattleSystem.instance.state == TutorialBattleState.PLAYATTACK)
        {
            if(TutorialBattleSystem.instance.hasAttackPlayed == false)
            {
                TutorialBattleSystem.instance.hasAttackPlayed = true;
                if(TutorialBattleSystem.instance.state == TutorialBattleState.BLANK || TutorialBattleSystem.instance.state == TutorialBattleState.PLAYATTACK)
                {
                    CardAttackSFX.Play();
                    switch (power)
                    {
                        case 1:
                        _animator.SetTrigger("1_Trigger");
                        break;
                        case 2:
                        _animator.SetTrigger("2_Trigger");
                        break;
                        case 3:
                        _animator.SetTrigger("3_Trigger");
                        break;
                        case 4:
                        _animator.SetTrigger("4_Trigger");
                        break;
                        case 5:
                        _animator.SetTrigger("5_Trigger");
                        break;
                        case 6:
                        _animator.SetTrigger("6_Trigger");
                        break;
                        case 7:
                        _animator.SetTrigger("7_Trigger");
                        break;
                        case 8:
                        _animator.SetTrigger("8_Trigger");
                        break;
                        default:
                        break;
                    }
                }
            }  
        }
    }
    public void DefendAnimation()
    {
    if(TutorialBattleSystem.instance.state == TutorialBattleState.PLAYBLOCK || TutorialBattleSystem.instance.state == TutorialBattleState.BLOCK) 
        {
            if(TutorialBattleSystem.instance.hasDefendPlayed == false)
            {
                TutorialBattleSystem.instance.hasDefendPlayed = true;    
                CardDefendSFX.Play();
                _animator.SetTrigger("Mago_Defend");
            }
        }
    }
    public void MethodAttackAnswer()
    {
        TutorialBattleSystem.instance.getSelectedCardPower(power);
        TutorialBattleSystem.instance.AttackAnswer();
    }
    public void MethodDefendAnswer()
    {
        TutorialBattleSystem.instance.getSelectedCardPower(power);
        TutorialBattleSystem.instance.DefendAnswer();
    }
}