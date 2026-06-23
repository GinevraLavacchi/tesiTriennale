using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlimeState : MonoBehaviour
{
    public Slime slime;
    public Transform player;
    public float attackRange = 1f;
    public Rigidbody2D rb;
    public bool isPlayerInRange=false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.slime = GetComponent<Slime>();
    }

    public virtual void Enter()
    {
        this.enabled = true;
    }

    public virtual void Exit()
    {
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject.transform;
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            player = null;
        }
    }

    public bool isPlayerInAttackRange()
    {
        if (player)
        {
            float distance= Vector2.Distance(transform.position, player.position);
            if (distance <= attackRange)
            {
                return true;
            } else
            {
                return false;
            }
        }
        return false;
    }

}
