using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private float blinkDuration = 0.1f;
    private int blinkTimes = 2;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed = 0.5f;
    public float timeUntilAttack= 0f;
    [SerializeField] private LayerMask slimeHitBox;
    [SerializeField] private LayerMask stoneHitBox;
    [SerializeField] private float attackRange = 1f;

    [SerializeField] private int attacksCounter = 0;
    [SerializeField] private PlayerEnergy playerEnergy;
 
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerEnergy = GetComponent<PlayerEnergy>();
    }

    public void Attack()
    {
        /*if (timeUntilAttack <= 0f)
        {*/
            attacksCounter++;
            timeUntilAttack = attackSpeed;
            AnimateAttack();
            DetectEnemies();

        if (attacksCounter == 5)
            {
                playerEnergy.UpdateEnergy(-0.5f);
                attacksCounter = 0;
            }
       /* }
        else
        {
            timeUntilAttack -= Time.deltaTime;
        }*/
        
    }

    public void DetectEnemies()
    {
        Collider2D[] slimeColliders = Physics2D.OverlapCircleAll(transform.position, attackRange, slimeHitBox);
        foreach (Collider2D collider in slimeColliders)
        {
            SlimeHealth health = collider.GetComponentInParent<SlimeHealth>();  
            if (collider.CompareTag("SlimeHitBox") && health != null)
            {
                if(health.health>0)
                {
                    health.TakeDamage(damage);
                    Player pl = gameObject.GetComponent<Player>();
                    if (pl != null)
                    {
                        pl.ToolUsed();
                    }
                }
            }
        }
    }

    public void DetectStones()
    {
        Collider2D[] stoneColliders = Physics2D.OverlapCircleAll(transform.position, attackRange, stoneHitBox);
        foreach (Collider2D collider in stoneColliders)
        {
            Stone stone = collider.GetComponentInParent<Stone>();
            if (collider.CompareTag("StoneHitBox") && stone != null)
            {
                stone.TakeHit();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void AnimateAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void TriggerAnimationEvent()  //Event inside the attack animation
    {
        animator.Play("Idle");
    }

    public void ShowBlinkEffect()
    {
        StartCoroutine(BlinkEffect());
    }

    private IEnumerator BlinkEffect()
    {
        for (int i = 0; i < blinkTimes; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
