using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackingState : SlimeState
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float attackCooldown = 2f;
    private bool canAttack = true;
    [SerializeField] private float damage = 10f;
    private bool canDealDamage;
    private float chargeTime = 0.5f;

    [SerializeField] private Animator animator;


    private void Update()
    {
        if (player)
        {
            if (canAttack)
            {
                StartCoroutine(Attack());
                //JumpTowardsPlayer();

                //transform.position = Vector2.Lerp(transform.position, player.position, Time.deltaTime); 
                canAttack = false;
                Invoke("ResetAttack", attackCooldown);
            }

            if (!isPlayerInAttackRange())
            {
                slime.chasing.Enter();
                Exit();
            }
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
        canDealDamage = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && canDealDamage)
        {
            collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-damage);
            canDealDamage = false;
        }
    }
    
    private IEnumerator Attack()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(chargeTime);
        JumpTowardsPlayer();
    }

    private void JumpTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);

        /*Vector2 targetPosition = player.position;
        float distance = speed * Time.deltaTime; // Velocità calcolata in base al tempo fisso
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);*/
    }


}
