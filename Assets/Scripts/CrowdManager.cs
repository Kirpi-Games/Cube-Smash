using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Common;
using Akali.Scripts.Managers;
using Akali.Scripts.Managers.StateMachine;
using DG.Tweening;
using UnityEngine;

public class CrowdManager : Singleton<CrowdManager>
{
    public List<GameObject> dummys;
    
    public void DummyAdd(GameObject dummy)
    {
        Taptic.Heavy();
        dummys.Add(dummy);
    }
    
    public void RemoveDummy(GameObject dummy)
    {
        Taptic.Heavy();
        dummys.Remove(dummy);
    }

    private void Awake()
    {
        GameStateManager.Instance.GameStatePlaying.OnExecute += FollowTarget;
    }
    public void FollowTarget()
    {
        for (int i = 0; i < dummys.Count; i++)
        {
            if (dummys[i].GetComponent<Dummys>().isCollect)
            {
                if (!dummys[i].GetComponent<Dummys>().isForward)
                {
                    var playerTransform = PlayerController.Instance.transform.position;
                    var TargetPos = playerTransform - Vector3.forward - Vector3.forward * i;
                    dummys[i].transform.position = Vector3.Lerp(dummys[i].transform.position,TargetPos, dummys[i].GetComponent<Dummys>().baseSpeed/(i+1) * Time.deltaTime);//Arkadan Takip
                }
                else
                {
                    var playerTransform = PlayerController.Instance.transform.position;
                    var TargetPos = playerTransform + Vector3.forward + Vector3.forward * i;
                    dummys[i].transform.position = Vector3.Lerp(dummys[i].transform.position,TargetPos, 30/(i+1) * Time.deltaTime);//Onden Takip
                }   
            }
        }
    }

    public void DeadDummys()
    {
        for (int i = 0; i < dummys.Count; i++)
        {
            dummys[i].transform.DOScale(0, 0.2f);
            var dummyParticle = AkaliPoolManager.Instance.Dequeue<DummyBlast>();
            dummyParticle.transform.position = dummys[i].transform.position;
        }
    }
    

    public void SetDummyForward()
    {
        for (int i = 0; i < dummys.Count; i++)
        {
            dummys[i].GetComponent<Dummys>().isForward = true;
        }
    }
    
    public void SetDummyBack()
    {
        for (int i = 0; i < dummys.Count; i++)
        {
            dummys[i].GetComponent<Dummys>().isForward = false;
        }
    }
}
