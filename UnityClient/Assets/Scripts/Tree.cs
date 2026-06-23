using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject woodDropPrefab;  // Prefab della legna da droppare
    public GameObject appleDropPrefab; //Prefab della mela
    public Transform dropPoint;        // Punto da cui la legna verrà generata
    public float respawnTime = 100f;     // Tempo prima che l'albero ricresca
    public float dropRadius = 1f;        // Raggio attorno all'albero in cui generare la legna
    public float miniDropDistance = 0.5f; //minimo raggio di spawn per evitare che spawni sul tronco
    public int numberOfDrops = 3;          // Numero di pezzi di legna da droppare

    private SpriteRenderer spriteRenderer;
    private Collider2D treeCollider;
    [Header("Sprite per le sostituzioni quando tagliato")]
    public Sprite BigcutTree;
    public Sprite BigTree;
    public Sprite LittlecutTree;
    public Sprite LittleTree;
    public Sprite AppleTree;
    public string oldTree;
    public bool isDown = false; //per controllare se è stato tagliato

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        treeCollider = GetComponent<Collider2D>();
    }

    // Metodo chiamato dal giocatore per abbattere l'albero
    public void CutDown()
    {
        if(!isDown)
        {
            //controllo che tipo di albero è per gestire del drop
            if(gameObject.name.StartsWith("Big"))
            {
                spriteRenderer.sprite = BigcutTree;
                oldTree = gameObject.name;

                // Droppa la legna
                DropWood();

            }
            else if((gameObject.name.StartsWith("Apple")))
            {
                spriteRenderer.sprite = BigcutTree;
                oldTree = gameObject.name;

                // Droppa la legna e le mele
                DropWoodAndApple();

            }
            else if((gameObject.name.StartsWith("Little")))
            {
                spriteRenderer.sprite = LittlecutTree;
                oldTree = gameObject.name;

                // Droppa la legna
                DropWood();
            }
            isDown= true;
            

            // Inizia la coroutine per far ricrescere l'albero dopo un po' di tempo
            StartCoroutine(RespawnTree());
        }
        else
        {
            Debug.Log("Albero già tagliato");
        }
    }

    // Metodo per droppare la legna
    private void DropWood()
    {
        for (int i = 0; i < numberOfDrops; i++)
        {
            // Ottieni una posizione valida per il drop
            Vector2 spawnPosition = GetRandomSpawnPosition(transform.position, miniDropDistance, dropRadius);

            // Instanzia il drop alla posizione calcolata
            Instantiate(woodDropPrefab, spawnPosition, Quaternion.identity);
        }
    }
    private void DropWoodAndApple()
    {
        for (int i = 0; i < numberOfDrops; i++)
        {
            // Ottieni una posizione valida per il drop
            Vector2 spawnPosition = GetRandomSpawnPosition(transform.position, miniDropDistance, dropRadius);

            // Instanzia il drop alla posizione calcolata
            Instantiate(woodDropPrefab, spawnPosition, Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            // Ottieni una posizione valida per il drop
            Vector2 spawnPosition = GetRandomSpawnPosition(transform.position, miniDropDistance, dropRadius+1f);

            // Instanzia il drop alla posizione calcolata
            Instantiate(appleDropPrefab, spawnPosition, Quaternion.identity);
        }
    }
        // Coroutine per far ricrescere l'albero
    private IEnumerator RespawnTree()
    {
        // Aspetta il tempo di respawn
        yield return new WaitForSeconds(respawnTime);
        if (oldTree.StartsWith("Big") )
        {
            spriteRenderer.sprite = BigTree;
        }
        else if (oldTree.StartsWith("Little"))
        {
            spriteRenderer.sprite = LittleTree;
        }
        else if(oldTree.StartsWith("Apple"))
        {
            spriteRenderer.sprite = AppleTree;
        }
        isDown = false;
    }

    // Funzione per generare un punto di spawn a distanza dall'albero
    Vector2 GetRandomSpawnPosition(Vector2 treePosition, float minDistance, float maxDistance)
    {
        // Variabile per conservare la posizione del drop
        Vector2 spawnPosition;

        // Ripetere finché il drop non si trova a una distanza valida
        do
        {
            // Genera un punto casuale all'interno di un cerchio di raggio maxDistance
            float angle = Random.Range(0f, Mathf.PI * 2); // Angolo casuale
            float distance = Random.Range(minDistance, maxDistance); // Distanza casuale tra min e max

            // Calcola la posizione usando angolo e distanza
            spawnPosition = new Vector2(
                treePosition.x + Mathf.Cos(angle) * distance,
                treePosition.y + Mathf.Sin(angle) * distance
            );

        } while (Vector2.Distance(spawnPosition, treePosition) < minDistance);  // Controlla che la distanza minima sia rispettata

        return spawnPosition;
    }


}
