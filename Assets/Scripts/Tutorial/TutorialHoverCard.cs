using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TutorialHoverCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        if(TutorialBattleSystem.instance.state == TutorialBattleState.PLAYERTURN || TutorialBattleSystem.instance.state == TutorialBattleState.BLOCK)
        {
        transform.position += Vector3.up * hoverHeight;
        if(TutorialBattleSystem.instance.round % 2 == 0)
        {
            _power = power.ToString();
            TutorialBattleSystem.instance.formulatext.text = "<color=#FFFFFF>" + _power + "</color>" + "-" + TutorialBattleSystem.instance.formula.ToString();
        }
        else
        {
        _power = power.ToString();
        TutorialBattleSystem.instance.formulatext.text = "<color=#FFFFFF>" + _power + "</color>" + "+" + TutorialBattleSystem.instance.formula.ToString();
        }
        }
    }
 
    public void OnPointerExit(PointerEventData eventData) 
    {
        transform.position = originalPosition;
        if(TutorialBattleSystem.instance.round % 2 == 0)
        {
            TutorialBattleSystem.instance.formulatext.text = "-" + TutorialBattleSystem.instance.formula.ToString();
        }
        else
        {
        TutorialBattleSystem.instance.formulatext.text = "+" + TutorialBattleSystem.instance.formula.ToString();
        }
    }
}