using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpanimation : MonoBehaviour
{
    private Vector2 originalPosition;
    
    void Awake()
    {
        originalPosition = transform.localPosition;
    }
    public void StartJumping()
    {
        transform.LeanMoveLocal(new Vector2(0,1000),0.5f).setEaseOutQuart();
    }
    public void Reset()
    {
        transform.LeanMoveLocal(new Vector2(0,-1000),1f).setEaseOutQuart();
    }
}
