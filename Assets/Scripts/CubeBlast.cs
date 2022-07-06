using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CubeBlast : MonoBehaviour
{

    public void DestroyCube()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.DOScale(0, 0.8f);
            Destroy(this.gameObject,0.8f);            
        }
    }
    
}
