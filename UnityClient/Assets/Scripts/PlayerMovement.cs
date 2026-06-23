using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody;
    private Vector2 moveDirection;
    public Animator animator;
    private Vector2 previousDir;
    private int action=0;
    public float flashDuration = 1f;  // Durata totale del lampeggio
    public float flashInterval = 0.2f; // Intervallo tra l'accensione e lo spegnimento
    public LayerMask workBench;
    public LayerMask chestLayer;
    public Player player;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
        player= GetComponent<Player>();
        ProcessInputs();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
        PlayerAttack plAttack = gameObject.GetComponent<PlayerAttack>();
        if (Input.GetMouseButton(0)&&player.tool=="hoe") //per le azioni se è 1 sta arando, se è 2 sta prendendo risorse, se è 3 attacca,5  se annaffia
        {
            animator.SetInteger("Action", 1);
            action = 1;
            rigidbody.velocity = Vector2.zero;

        }
        if (Input.GetMouseButton(0) && player.tool == "axe") //per le azioni se è 1 sta arando, se è 2 sta disboscando, se è 3 attaccando, 4 se picconando
        {
            animator.SetInteger("Action", 2);
            action = 2;
        }
        if(Input.GetMouseButton(0) && player.tool == "sword")
        {
            animator.SetInteger("Action", 3);
        }
        //MANCA IL PICCONE
        if (Input.GetMouseButton(0) && player.tool == "pickaxe")
        {
            animator.SetInteger("Action", 4);
            // plAttack.DetectStones();
        }
        /*if (Input.GetKey(KeyCode.E))
        {
            Collider2D[] works = Physics2D.OverlapCircleAll(transform.position, 0.5f, workBench);

            foreach (Collider2D bench in works)
            {
                WorkBench benchScript = bench.GetComponent<WorkBench>();

                if (benchScript != null)
                {
                    benchScript.Working();
                    break; 
                }
            }
            Collider2D[] chests= Physics2D.OverlapCircleAll(transform.position, 0.5f, chestLayer);
            foreach (Collider2D chest in chests)
            {
                Chest chestScript = chest.GetComponent<Chest>();

                if (chestScript != null)
                {
                    chestScript.Open();
                    break;
                }
            }*/
        //}
    }
    private void OnDrawGizmos()
    {
        // Imposta il colore del cerchio
        Gizmos.color = Color.red;

        // Disegna il cerchio attorno alla posizione del giocatore
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
    public void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        
        if((moveX == 0 &&  moveY ==0)  && moveDirection.x != 0 || moveDirection.y != 0)
        {
            previousDir = moveDirection; 
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    public void Move()
    {
        rigidbody.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        animator.SetInteger("Action", 10);
        action = 10;
    }

    public void Animate()
    {
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);
        animator.SetFloat("previousDir.x", previousDir.x);
        animator.SetFloat("previousDir.y", previousDir.y);
    }
    public void EndAnimation()
    {
        StartCoroutine(wait());
    }
    public IEnumerator wait()
    {
        yield return new WaitForSeconds(0.2f);
        animator.Play("Idle");
    }
    
}
