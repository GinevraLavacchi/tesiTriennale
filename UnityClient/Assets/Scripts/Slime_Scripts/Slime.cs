using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ChasingState))]
[RequireComponent(typeof(AttackingState))]
[RequireComponent(typeof(SleepingState))]

public class Slime : MonoBehaviour
{
    public SleepingState sleeping;
    public AttackingState attacking;
    public ChasingState chasing;

    public SlimeState initialstate;

    private void Start()
    {
        this.chasing = GetComponent<ChasingState>();
        this.attacking = GetComponent<AttackingState>();
        this.sleeping = GetComponent<SleepingState>();

        ResetState();
    }

    public void ResetState()
    {
        this.sleeping.Exit();
        this.chasing.Exit();
        this.attacking.Exit();

        if (initialstate != null)
        {
            initialstate.Enter();
        }
    }
}
