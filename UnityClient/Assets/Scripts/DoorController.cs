using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorController : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase staticTile;
    public TileBase openTile;
    private Vector3Int position = new Vector3Int(17, -2, 0);
    public FadingSprite wallCollider;
    public static bool isIn;
    public IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        tilemap.SetTile(position, staticTile);  // Sostituisce con una tile statica
    }
    private void Start()
    {
        tilemap.SetTile(position, staticTile);

    }
    public void Open()
    {
        tilemap.SetTile(position, openTile);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player") 
        {
            if(this.CompareTag("EnterTrigger")) //se ha attivato il trigger per entrare 
            {
                isIn = true;
                Open();
                wallCollider.Entered();
                StartCoroutine(StopAnimation());
            }
            else if(this.CompareTag("ExitTrigger")&&isIn) //se ha attivato il trigger per uscire ed era stato effettivamente dentro
            {
                isIn = false;
                wallCollider.Out();
            }
        }
    }

}
