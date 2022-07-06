using System.Collections;
using System.Collections.Generic;
using Akali.Common;
using DG.Tweening;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public GameObject target;
    public float smoothSpeed; 
    public Vector3 offset; 
    public Vector3 lookatOffset,finalOffset; 
    public bool isFollow,isFinal; 
    public GameObject newTarget;
    private float offsetZ = -7;
    
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        isFollow = true;
           
    }
        
    public void IncreaseOffsetZ()
    {
        DOTween.To(()=> offsetZ, x=> offsetZ = x, offsetZ-1, 0.7f);
    }
    
    public void DecreaseOffsetZ()
    {
        DOTween.To(()=> offsetZ, x=> offsetZ = x, offsetZ+1, 0.7f);
    }
        
    public void CameraFollow()
    {
        if (target == null) return;
        
        if (target != null)
        {
            offset.z = offsetZ;
            Vector3 desiredPosition = new Vector3(target.transform.position.x,target.transform.position.y,target.transform.position.z) + offset;
            Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothed;
            transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            Vector3 lookAtTarget = new Vector3(target.transform.position.x,0,target.transform.position.z) + lookatOffset;
            //transform.LookAt(new Vector3(lookAtTarget.x,lookAtTarget.y,lookAtTarget.z));    
        }
        
    }
        
    private void LateUpdate()
    {
                
        if (target == null)
        {
            return;
        }
        
        if (isFollow)
        {
            CameraFollow();
        }
    }
}
