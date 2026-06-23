using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadingSprite : MonoBehaviour
{
    public TilemapRenderer tilemapRenderer;
    public GameObject colliderEsterno;
    public GameObject colliderInterno;
    public GameObject HouseUpWall;//muro sopra per camminarci dietro
    public GameObject HouseUpWall1;//muro sopra per camminarci davanti
    public GameObject HouseDownWall;//muro sotto per camminarci davanti
    public GameObject HouseDownWall1;//muro sotto per camminarci dietro

    private void Start()
    {
        HouseDownWall.SetActive(false);
        
        colliderEsterno.SetActive(true);
        colliderInterno.SetActive(false);
    }
    public void Entered()
    {
        tilemapRenderer.enabled = false;
        HouseUpWall.SetActive(true);
        HouseUpWall1.SetActive(false);
        HouseDownWall.SetActive(true);
        HouseDownWall1.SetActive(false);
        colliderInterno.SetActive(true);
        colliderEsterno.SetActive(false);
    }
    public void Out()
    {
        tilemapRenderer.enabled = true;
        HouseUpWall1.SetActive(true);
        HouseUpWall.SetActive(false);
        HouseDownWall1.SetActive(true);
        HouseDownWall.SetActive(false);
        colliderInterno.SetActive(false);
        colliderEsterno.SetActive(true);
    }
    
}
