using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoncBehavior : MonoBehaviour
{
    private bool _isWaitingFruits = false;

    private Animator _animator;
    
    private static readonly int IsWaitingForFood = Animator.StringToHash("IsWaitingForFood");
    private static readonly int Chew = Animator.StringToHash("Chew");
    private static readonly int GoIdle = Animator.StringToHash("GoIdle");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        InvokeRepeating(nameof(CheckForOneActiveBubble), 0, 1);
        EventManager.RegisterListener<BabyFedEventInfo>(BabyFed);

    }

    private void BabyFed(EventInfo eventinfo)
    {
        _animator.SetTrigger(Chew);
        Invoke(nameof(StopChewing),1.5f);
    }

    private void StopChewing()
    {
        _animator.SetTrigger(GoIdle);
    }

    void CheckForOneActiveBubble()
    {
        if (GameObject.FindWithTag("Bubble"))
        {
            _animator.SetBool(IsWaitingForFood, true);
        }
        else
        {
            _animator.SetBool(IsWaitingForFood, false);
        }   
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<BabyFedEventInfo>(BabyFed);

    }
}
