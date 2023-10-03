using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    public Animator animator;

    public void FireAnimation()
    {
        animator.SetTrigger("MagoAttacked");
    }
}
