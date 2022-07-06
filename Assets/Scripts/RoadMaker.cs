using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

public class RoadMaker : MonoBehaviour
{
    public enum Type
    {
        bridge,ladder,slider
    }

    public Type type;
    
    public List<GameObject> targetRoad;
    [SerializeField]private int index = -1; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (index > targetRoad.Count)
            {
                other.transform.DOScale(Vector3.zero, 0.2f);
                other.GetComponent<Dummys>().isCollect = false;
                var dummyParticle = AkaliPoolManager.Instance.Dequeue<DummyBlast>();
                dummyParticle.transform.position = other.gameObject.transform.position;
                CrowdManager.Instance.RemoveDummy(other.gameObject);
            }
            else
            {
                other.GetComponent<Dummys>().isCollect = false;
                other.GetComponent<Dummys>().animator.SetBool("isRun",false);
                CrowdManager.Instance.RemoveDummy(other.gameObject);
                SetDummyToTarget(other);    
            }
        }

        if (other.gameObject.layer == 3)
        {
            other.transform.DOMoveX(0, 0.2f);
            other.GetComponent<PlayerController>().swerveSpeed = 0;
            if (index < targetRoad.Count-1)
            {
                AkaliLevelManager.Instance.LevelIsFail();
                other.GetComponent<Animator>().SetTrigger("Cry");
                other.GetComponent<CrowdManager>().DeadDummys();
            }
        }
    }

    void SetDummyToTarget(Collider other)
    {
        index++;
        switch (type)
        {
            case Type.ladder:
                if (index < targetRoad.Count)
                {
                    other.transform.DOMove(targetRoad[index].transform.position,0.4f).OnComplete((() => other.GetComponent<Dummys>().animator.SetBool("isRun",false))).OnComplete((() => other.gameObject.layer = 9));
                    other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                }
                break;
            case Type.bridge:
                if (index < targetRoad.Count)
                {
                    other.transform.DOMove(targetRoad[index].transform.position,0.6f).OnComplete((() => other.GetComponent<Dummys>().animator.SetBool("isRun",false))).OnComplete((() =>other.transform.DORotate(new Vector3(90,0,0),0.2f)));
                    other.gameObject.layer = 11;
                    other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                }
                break;
            case Type.slider:
                if (index < targetRoad.Count)
                {
                    other.transform.DOMove(targetRoad[index].transform.position,0.6f).OnComplete((() => other.GetComponent<Dummys>().animator.SetBool("isRun",false))).OnComplete((() =>other.transform.DORotate(new Vector3(103.23f,0,0),0.2f)));
                    other.gameObject.layer = 12;
                    other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                }
                break;
        }
    }
}
