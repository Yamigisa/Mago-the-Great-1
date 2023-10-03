using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BookHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 scaleFactor;

    public void Start()
    {
        scaleFactor = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData) 
    {
        transform.Rotate(Vector3.forward * 20); // or whatever roatation you want
        transform.localScale *= 1.3f;
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        transform.Rotate(Vector3.forward * -20);
        transform.localScale = scaleFactor;
    }
}
