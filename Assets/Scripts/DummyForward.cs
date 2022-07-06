using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers.StateMachine;
using UnityEngine;

public class DummyForward : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            print("HOP");
            other.GetComponent<CrowdManager>().SetDummyForward();
        }
    }
}
