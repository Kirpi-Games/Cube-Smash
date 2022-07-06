using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers;
using Akali.Scripts.Managers.StateMachine;
using DG.Tweening;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public bool isThrow;
    public Transform hammerParent;
    public float hammerSpeed;
    public float hammerBackSpeed;
    public Transform target;
    public bool canTarget;
    public GameObject hammerChild;


    private void Awake()
    {
        GameStateManager.Instance.GameStatePlaying.OnExecute += UpdateHammerPos;
    }

    public void HammerThrow()
    {
        if (canTarget)
        {
            transform.SetParent(null);
            transform.DOMove(target.position, hammerSpeed).SetEase(Ease.Linear).OnComplete((() => HammerBackParent()));
        }
    }

    public void HammerBackParent()
    {
        canTarget = false;
    }

    private void UpdateHammerPos()
    {
        if (!canTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position,hammerParent.position,hammerBackSpeed * Time.deltaTime);
            if (transform.position == hammerParent.transform.position)
            {
                transform.SetParent(hammerParent);
                if (transform.parent == hammerParent)
                {
                    transform.localPosition = Vector3.zero;
                    transform.DOLocalRotate(Vector3.zero, 0.2f);
                    GetComponent<BoxCollider>().enabled = true;
                }    
            }
                
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            GetComponent<BoxCollider>().enabled = false;
            other.transform.parent.DOMove(other.transform.parent.position + new Vector3(0,-2f,0), 0.1f).SetEase(Ease.Linear);
            isThrow = false;
            Destroy(other.gameObject,0.1f);
            other.transform.DOScale(0, 0.1f);
            Debug.Log("wow");
            var cubeParticle = AkaliPoolManager.Instance.Dequeue<CubeBlast>();
            cubeParticle.transform.position = other.gameObject.transform.position;
        }

        if (other.gameObject.layer == 13)
        {
            other.GetComponent<FinalDummyCharacter>().FinalMove();
            other.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
