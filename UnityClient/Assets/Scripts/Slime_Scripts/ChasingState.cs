using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChasingState : SlimeState
{
    [SerializeField] private float speed = 5f;
    private Vector2 direction;
    private Animator animator;
    private bool isMoving;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transitionToSleeping();
        transitionToAttacking();
        AnimateMovement();
    }

    private void FixedUpdate()
    {
        Debug.Log("lo slime mi insegueeeee");
        if (player)
        {
            isMoving = true;
            rb.velocity = new Vector2(direction.x, direction.y)*speed;
        }
    }

    public void transitionToSleeping()
    {
        if (!player)
        {
            slime.sleeping.Enter();
            rb.velocity = Vector2.zero;
            isMoving = false;
            Exit();
        }
        else
        {
            direction = (player.position - transform.position).normalized;
        }
    }

    public void transitionToAttacking()
    {
        if(isPlayerInAttackRange())
        {
            slime.attacking.Enter(); 
            rb.velocity = Vector2.zero;
            Exit();
        }
    }

    public void AnimateMovement()
    {
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetBool("isMoving", isMoving);
    }
}
