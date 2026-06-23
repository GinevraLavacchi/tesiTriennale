using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    [SerializeField] public int growthDegree;
    [SerializeField] public Vector3 position;
    [SerializeField] public GameObject tomatoDrop;
    [SerializeField] private Sprite level1;
    [SerializeField] private Sprite level2;
    [SerializeField] private Sprite level3;
    [SerializeField] private Sprite level4;
    [SerializeField] public Sprite dropSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Transform dropPoint;
    public float dropRadius = 1f;
    public float miniDropDistance = 0.5f;
    private int numberOfDrops = 2;
    public bool isOkToDrop=false;

    public void Awake()
    {
        if (gameObject.tag == "Tomato" || gameObject.layer == LayerMask.NameToLayer("Tomato"))
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = level1;
        growthDegree = 1;
    }
    public void ChangeSprite()
    {
        growthDegree++;
        

    }
    public void Update()
    {
        switch (growthDegree)
        {
            case 1:
                spriteRenderer.sprite = level1;
                break;

            case 2:
                spriteRenderer.sprite = level2;
                break;
            case 3:
                spriteRenderer.sprite = level3;
                break;
            case 4:
                spriteRenderer.sprite = level4;
                isOkToDrop = true;
                break;
        }

    }
    public void DropTomato()
    {
        if (growthDegree == 4)
        {
            for (int i = 0; i < numberOfDrops; i++)
            {
                // Ottieni una posizione valida per il drop
                Vector2 spawnPosition = GetRandomSpawnPosition(transform.position, miniDropDistance, dropRadius);

                // Instanzia il drop alla posizione calcolata
                Instantiate(tomatoDrop, spawnPosition, Quaternion.identity);
            }
        }
    }
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
