using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

public class FinalDummyCharacter : MonoBehaviour
{
    public void FinalMove()
    {
        float z = Random.Range(70,170);
        Vector3 direction = new Vector3(0, 0, z);
        transform.DOJump(transform.position + direction, 7, 2, 3).OnComplete((() => AkaliLevelManager.Instance.LevelIsCompleted()));
        CameraController.Instance.target = this.gameObject;
        GetComponent<Animator>().SetBool("isDead",true);
    }
}
