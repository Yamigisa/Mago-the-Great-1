using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardHoverTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        if(BattleSystem.instance.state == BattleState.PLAYERTURN || BattleSystem.instance.state == BattleState.BLOCK)
        {
        transform.position += Vector3.up * hoverHeight;
        if(BattleSystem.instance.round % 2 == 0)
        {
            _power = power.ToString();
            BattleSystem.instance.formulatext.text = "<color=#FFFFFF>" + _power + "</color>" + "-" + BattleSystem.instance.formula.ToString();
        }
        else
        {
        _power = power.ToString();
        BattleSystem.instance.formulatext.text = "<color=#FFFFFF>" + _power + "</color>" + "+" + BattleSystem.instance.formula.ToString();
        }
        }
    }
 
    public void OnPointerExit(PointerEventData eventData) 
    {
        transform.position = originalPosition;
        if(BattleSystem.instance.round % 2 == 0)
        {
            BattleSystem.instance.formulatext.text = "-" + BattleSystem.instance.formula.ToString();
        }
        else
        {
        BattleSystem.instance.formulatext.text = "+" + BattleSystem.instance.formula.ToString();
        }
    }
}