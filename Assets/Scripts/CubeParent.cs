using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers.StateMachine;
using DG.Tweening;
using UnityEngine;

public class CubeParent : MonoBehaviour
{
    public int boxCount;
    public GameObject[] dummys;

    private void Awake()
    {
        GameStateManager.Instance.GameStatePlaying.OnExecute += BoxCount;
    }
    
    private void BoxCount()
    {
        boxCount = transform.childCount - 1;
        if (boxCount == 0)
        {
            SetDummyToPlayer();
            GameStateManager.Instance.GameStatePlaying.OnExecute -= BoxCount;
        }
    }

    public void SetDummyToPlayer()
    {
        for (int i = 0; i < dummys.Length; i++)
        {
            dummys[i].transform.SetParent(null);
            dummys[i].GetComponent<Dummys>().isCollect = true;
            dummys[i].GetComponent<Dummys>().animator.SetBool("isRun",true);
            dummys[i].transform.DORotate(new Vector3(0, 0, 0), 0.1f);
            CrowdManager.Instance.DummyAdd(dummys[i]);
        }
    }
    
}
