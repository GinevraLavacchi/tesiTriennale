using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHealth : MonoBehaviour
{
    [SerializeField] public float health;
    private Animator animator;
    private bool isDead;

    [SerializeField] private Rigidbody2D rb; //Trying to not let the player push the enemy

    [SerializeField] private GameObject drop;
    [SerializeField] private int minDrops = 1;
    [SerializeField] private int maxDrops = 3;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool("isDead", isDead);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("Hit");

        if (health <= 0f)
        {
            rb.bodyType= RigidbodyType2D.Kinematic; //Trying to not let the player push the enemy
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        int numDrops = Random.Range(minDrops, maxDrops + 1);

        for (int i = 0; i < numDrops; i++)
        {
            Vector3 dropPosition = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            Instantiate(drop, dropPosition, Quaternion.identity);
        }
    }

    public void TriggerAnimationEventHit() 
    {
        animator.Play("Sleeping");
    }

    public void TriggerAnimationEventDeath() 
    {
        Destroy(gameObject);
    }
}
