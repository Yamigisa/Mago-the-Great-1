using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ThisCard : MonoBehaviour
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
    public void AttackDefendAnimation()
    {
        if(BattleSystem.instance.state == BattleState.BLANK || BattleSystem.instance.state == BattleState.PLAYATTACK)
        {
            if(BattleSystem.instance.hasAttackPlayed == false)
            {
                BattleSystem.instance.hasAttackPlayed = true;
                if(BattleSystem.instance.state == BattleState.BLANK || BattleSystem.instance.state == BattleState.PLAYATTACK)
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
        if(BattleSystem.instance.state == BattleState.PLAYBLOCK || BattleSystem.instance.state == BattleState.BLOCK) 
        {
            if(BattleSystem.instance.hasDefendPlayed == false)
            {
                BattleSystem.instance.hasDefendPlayed = true;    
                CardDefendSFX.Play();
                _animator.SetTrigger("Mago_Defend");
            }
        }
    }
    public void MethodAttackAnswer()
    {
        BattleSystem.instance.getSelectedCardPower(power);
        BattleSystem.instance.AttackAnswer();
    }
    public void MethodDefendAnswer()
    {
        BattleSystem.instance.getSelectedCardPower(power);
        BattleSystem.instance.DefendAnswer();
    }
}