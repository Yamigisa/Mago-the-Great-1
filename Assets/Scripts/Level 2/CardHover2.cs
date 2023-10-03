using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardHover2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverHeight = 1f; // The amount of upward movement when the card is hovered over
    private Vector3 originalPosition; // The original position of the card
    Vector3 cachedScale;
    public Card _CardSO;
    public int id;
    public int power;
    public string _power;
    
    private void Awake()
    {
        id = _CardSO.id;
        power = _CardSO.power;
    }
    void Start() 
    {    
        originalPosition = transform.position;
    }
    
    public void OnPointerEnter(PointerEventData eventData) 
    {
        if(BattleSystem2.instance.state == BattleState2.PLAYERTURN || BattleSystem2.instance.state == BattleState2.BLOCK)
        {
        transform.position += Vector3.up * hoverHeight;
        if(BattleSystem2.instance.round % 2 == 0)
        {
            _power = power.ToString();
            BattleSystem2.instance.formulatext.text = "<color=#FFFFFF>" + _power + "</color>" + "-" + BattleSystem2.instance.formula.ToString();
        }
        else
        {
        _power = power.ToString();
        BattleSystem2.instance.formulatext.text = "<color=#FFFFFF>" + _power + "</color>" + "+" + BattleSystem2.instance.formula.ToString();
        }
        }
    }
 
    public void OnPointerExit(PointerEventData eventData) 
    {
        transform.position = originalPosition;
        if(BattleSystem2.instance.round % 2 == 0)
        {
            BattleSystem2.instance.formulatext.text = "-" + BattleSystem2.instance.formula.ToString();
        }
        else
        {
        BattleSystem2.instance.formulatext.text = "+" + BattleSystem2.instance.formula.ToString();
        }
    }
}