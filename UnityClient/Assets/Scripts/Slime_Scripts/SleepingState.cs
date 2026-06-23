using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingState : SlimeState
{
    private void Update()
    {
        if (isPlayerInRange) //if (player) 
        {
            slime.chasing.Enter();
            Exit();
        }
    }
}
