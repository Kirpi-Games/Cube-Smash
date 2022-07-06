using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public const string Run = "Run";
    public const string Climb = "Climb";
    public const string Slide = "Slide";
    public const string ThrowAxe = "Axe";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void RunAnimation(bool x)
    {
        animator.SetBool(ThrowAxe,!x);
        animator.SetBool(Climb,!x);
        animator.SetBool(Run,x);
        animator.SetBool(Slide,!x);
    }
    
    public void SlideAnimation(bool x)
    {
        animator.SetBool(ThrowAxe,!x);
        animator.SetBool(Climb,!x);
        animator.SetBool(Run,!x);
        animator.SetBool(Slide,x);
    }

    public void ClimbAnimation(bool x)
    {
        animator.SetBool(ThrowAxe,!x);
        animator.SetBool(Climb,x);
        animator.SetBool(Run,!x);
        animator.SetBool(Slide,!x);
    }

    public void Throw()
    {
        animator.SetTrigger(ThrowAxe);
    } 
}
