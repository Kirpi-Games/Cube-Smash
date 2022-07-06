using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers.StateMachine;
using DG.Tweening;
using UnityEngine;

public class Dummys : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public bool isCollect,isForward;
    public Animator animator;
    public float baseSpeed = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        GameStateManager.Instance.GameStatePlaying.OnExecute += SetSpeed;
    }

    public void SetSpeed()
    {
        if (isCollect)
        {
            DOTween.To(()=> baseSpeed, x=> baseSpeed = x, 30, 3f).OnComplete((() => GameStateManager.Instance.GameStatePlaying.OnExecute -= SetSpeed));    
        }
    }
}
