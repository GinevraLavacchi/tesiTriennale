using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    private float energy = 0f;
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private Color exhaustionColor;
    private float exhaustionEffectDuration = 0.2f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private int numOfStars;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite halfStar;
    [SerializeField] private Sprite emptyStar;

    private void Start()
    {
        energy = maxEnergy;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        numOfStars = stars.Length;
    }

    private void Update()
    {
        UpdateStars();
    }

    public void UpdateStars()
    {
        if (energy > numOfStars)
        {
            energy = numOfStars;
        }

        for (int i = 0; i < stars.Length; i++)
        {
            if (i < energy - 0.5f)
            {
                stars[i].sprite = fullStar;
            }
            else if (i < energy)
            {
                stars[i].sprite = halfStar;
            }
            else
            {
                stars[i].sprite = emptyStar;
            }
        }
    }

    public void UpdateEnergy(float points)
    {
        energy += points;
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
        else if (energy <= 0)
        {
            energy = 0f;
            ShowExhaustionEffect();
        }
    }

    public void ShowExhaustionEffect()
    {
        spriteRenderer.color = exhaustionColor;

        Invoke("ResetColor", exhaustionEffectDuration);
    }

    private void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }
}
