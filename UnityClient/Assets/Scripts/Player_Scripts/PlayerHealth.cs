using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;                                 

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]private float health = 0f;
    [SerializeField] private float maxHeath = 100f;
    [SerializeField] private Color deathColor;
    private float deathEffectDuration = 0.2f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private int numOfHearts;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Vector3 spawnLocation;
    //[SerializeField] private PlayerGathering inventory;
    private void Start()
    {
        health = maxHeath;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        numOfHearts = hearts.Length;
        //inventory = GetComponent<PlayerGathering>();
    }

    private void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.E))
        {
            eat();
        }*/

        UpdateHearts();
    }

    public void UpdateHearts()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health - 0.5f)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i < health)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    public bool isMaxHealth()
    {
        return health == maxHeath;
    }
    /*public void eat()
    {
        if (inventory.apple > 0)
        {
            UpdateHealth(0.5f);
            inventory.apple--;
        }
    }*/

    public void UpdateHealth(float points)
    {
        health += points;
        gameObject.GetComponent<PlayerAttack>().ShowBlinkEffect();
        if (health > maxHeath)
        {
            health= maxHeath;
        } else if (health <=0)
        {
            health = 0f;
            ShowDeathEffect();
            StartCoroutine(Die());/*
            SceneManager.LoadScene("SampleScene");
            gameObject.transform.position = spawnLocation;*/
        }
    }
    public void DeadTransition()
    {
        GameObject workbench = GameObject.FindWithTag("Desk");
        GameObject chest = GameObject.FindWithTag("Chest");
        PlayerHarvest plh = gameObject.GetComponent<PlayerHarvest>();

        GameObject[] wheats = GameObject.FindGameObjectsWithTag("Wheat");
        GameObject[] tomatoes = GameObject.FindGameObjectsWithTag("Tomato");
        SceneManager.LoadScene("SampleScene");
        gameObject.transform.position = new Vector3(17.5f, -3);
        plh.enabled = true;
        chest.GetComponent<SpriteRenderer>().enabled = true;
        chest.GetComponent<Collider2D>().enabled = true;
        workbench.GetComponent<SpriteRenderer>().enabled = true;
        workbench.GetComponent<Collider2D>().enabled = true;
        foreach (var w in wheats)
        {
            w.GetComponent<SpriteRenderer>().enabled = true;
            w.GetComponent<Collider2D>().enabled = true;
        }
        foreach (var t in tomatoes)
        {
            t.GetComponent<SpriteRenderer>().enabled = true;
            t.GetComponent<Collider2D>().enabled = true;
        }
    }
    public IEnumerator Die()
    {
        health = 5f;
        yield return new WaitForSeconds(0.8f);

        DeadTransition();
    }
    public void ShowDeathEffect()
    {
        spriteRenderer.color = deathColor;

        Invoke("ResetColor", deathEffectDuration);

    }

    private void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }
}
