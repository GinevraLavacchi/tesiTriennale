using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerHarvest : MonoBehaviour,DataPersistanceInterface
{
    [System.Serializable]
    public class PlantPref 
    {
        public int growthDegree;
        public Vector3 position;

        public PlantPref(int gr, Vector3 pos)
        {
            growthDegree = gr;
            position = pos;
        }

    }
    public float wateringRadius = 2f; // Raggio in cui il giocatore annaffia
    public FieldManager tileManager;
    public Animator animator;
    public int water = 0;
    public GameObject wheatPrefab;
    public GameObject tomatoPrefab;
    private Wheat wheat;
    private Tomato tomato;
    public Player player;
    [SerializeField] private LayerMask wheatLayer;
    [SerializeField] private LayerMask tomatoLayer;
    public GameObject ChestPanel;
    [SerializeField] private List<Wheat> wheats;
    private void Start()
    {
        player = GetComponent<Player>();
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        tileManager = gameManager.GetComponent<FieldManager>();
        animator=transform.GetComponent<Animator>();
        wheat = wheatPrefab.GetComponent<Wheat>();
        tomato=tomatoPrefab.GetComponent<Tomato>();
        
    }
    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        

        // Ottieni il nome della scena
        string sceneName = currentScene.name;
        if (sceneName=="SampleScene")
        {
            GameObject gameManager = GameObject.FindWithTag("GameManager");
            tileManager = gameManager.GetComponent<FieldManager>();

        }
        int life = 0;
        // Controlla se il giocatore preme il tasto di raccolta
        for (int i = 0; i < player.toolList.Count; i++)
        {
            if (player.toolList[i].type == "hoe")
            {
                life = player.toolList[i].life;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)&&water>0)
        {
            animator.SetInteger("Action", 5);
            Vector3Int position=ConvertToVector3Int(transform.position);
            if (tileManager.IsWaterable(position))
            {

                GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
                AudioManager aum = audiomen.GetComponent<AudioManager>();
                aum.PlaySoundEffect(aum.waterSound);
                water--;
                tileManager.SetWatered(position);
                tileManager.IsBorder(position);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)&&water<=0)
        {
            animator.SetInteger("Action", 5);
            Vector3Int position = ConvertToVector3Int(transform.position);
            if (tileManager.RefillWater(position))
            {
                GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
                AudioManager aum = audiomen.GetComponent<AudioManager>();
                aum.PlaySoundEffect(aum.waterSound);
                water = 20;
            }
            Debug.Log("Acqua terminata");
        }
        if (Input.GetMouseButton(0))
        {
            Vector3Int position = ConvertToVector3Int(transform.position);
            if (player.tool == "hoe" && life > 0)
            {
                GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
                AudioManager aum = audiomen.GetComponent<AudioManager>();
                aum.PlaySoundEffect(aum.collisionCollectSound);
                if (tileManager.IsZappable(position))
                {
                    tileManager.SetWorked(position);
                    player.ToolUsed();
                }
            }
            else
            {
                Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, 0.5f, wheatLayer);
                Collider2D[] items1 = Physics2D.OverlapCircleAll(transform.position, 0.5f, tomatoLayer);
                if (items.Length > 0)
                {
                    foreach (Collider2D item in items)
                    {
                        Wheat wheatScript = item.GetComponent<Wheat>();
                        
                        if (wheatScript != null)
                        {
                            if (wheatScript.isOkToDrop)
                            {
                                wheatScript.DropWheat();
                                
                                Destroy(item.gameObject);
                                tileManager.SetNormal(position);
                                tileManager.IsBorder(position);
                            }
                        }
                    }
                }
                if(items1.Length > 0)
                {
                    foreach (Collider2D item in items1)
                    {
                        Tomato tomatoScript = item.GetComponent<Tomato>();

                        if (tomatoScript != null)
                        {
                            if (tomatoScript.isOkToDrop)
                            {
                                Debug.Log(tomatoScript.isOkToDrop);
                                tomatoScript.DropTomato();
                                Destroy(item.gameObject);
                                tileManager.SetNormal(position);
                                tileManager.IsBorder(position);
                            }
                        }
                    }
                }
            }

            
        }
    }
    private void OnDrawGizmos()
    {
        // Imposta il colore del cerchio
        Gizmos.color = Color.red;

        // Disegna il cerchio attorno alla posizione del giocatore
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
    Vector3Int ConvertToVector3Int(Vector3 vector)
    {
        return new Vector3Int(
            Mathf.CeilToInt(vector.x)-1,
            Mathf.CeilToInt(vector.y) - 1,
            Mathf.CeilToInt(vector.z)
        );
    
    }
    public bool Plant(string seed)
    {
        if (seed== "wheatSeed")
        {
            Vector3Int position = ConvertToVector3Int(transform.position);
            if (tileManager.IsPlantable(position))
            {
                Vector3Int cellPosition = tileManager.tilemap.WorldToCell(transform.position);
                Vector3 cellCenterPos = tileManager.tilemap.GetCellCenterWorld(cellPosition);

                Instantiate(wheatPrefab, cellCenterPos, Quaternion.identity);
                
                return true;
            }

        }
        else if(seed=="tomatoSeed")
        {
            Vector3Int position = ConvertToVector3Int(transform.position);
            if (tileManager.IsPlantable(position))
            {
                Vector3Int cellPosition = tileManager.tilemap.WorldToCell(transform.position);
                Vector3 cellCenterPos = tileManager.tilemap.GetCellCenterWorld(cellPosition);

                Instantiate(tomatoPrefab, cellCenterPos, Quaternion.identity);
                return true;
            }
        }
        return false;
    }
    public bool FertilizePlant(string type)
    {
        if(type== "wheatFertilizer")
        {
            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, 0.5f, wheatLayer);
            Vector3Int position = ConvertToVector3Int(transform.position);
            if (items.Length >= 0)
            {
                foreach (Collider2D item in items)
                {
                    Wheat wheatScript = item.GetComponent<Wheat>();
                    
                    if (wheatScript != null)
                    { 
                        //Debug.Log("in " + type);
                        wheatScript.ChangeSprite();

                        if (wheatScript.isOkToDrop)
                        {
                            tileManager.SetNormal(position);
                        }
                        return true;
                    }
                }
            }
        }
        else if(type=="tomatoFertilizer")
        {
            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, 0.5f, tomatoLayer);
            Vector3Int position = ConvertToVector3Int(transform.position);
            if (items.Length > 0)
            {
                foreach (Collider2D item in items)
                {
                    Tomato tomatoScript= item.GetComponent<Tomato>();
                    if (tomatoScript != null)
                    {

                            tomatoScript.ChangeSprite();
                        if (tomatoScript.isOkToDrop)
                        {
                            tileManager.SetNormal(position);
                        }
                            return true;
                    }
                }
            }
        }
        return false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name.StartsWith("DropWheat"))
        {

            GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
            AudioManager aum = audiomen.GetComponent<AudioManager>();
            aum.PlaySoundEffect(aum.collisionCollectSound);
            if (player.CanAddItem("WheatDrop"))
            {
                player.inventory.AddSlot("WheatDrop", wheat.dropSprite);
                Destroy(collision.gameObject);
            }
            else
            {
                ChestUI chestScript=ChestPanel.GetComponent<ChestUI>();
                chestScript.Append("WheatDrop", wheat.dropSprite);
                Destroy(collision.gameObject);
            }
        }
        if (collision.name.StartsWith("DropTomato"))
        {

            GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
            AudioManager aum = audiomen.GetComponent<AudioManager>();
            aum.PlaySoundEffect(aum.collisionCollectSound);
            if (player.CanAddItem("TomatoDrop"))
            {
                player.inventory.AddSlot("TomatoDrop", tomato.dropSprite);
                Destroy(collision.gameObject);
            }
            else
            {
                ChestUI chestScript = ChestPanel.GetComponent<ChestUI>();
                chestScript.Append("TomatoDrop", tomato.dropSprite);
                Destroy(collision.gameObject);
            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.name.StartsWith("DropWheat"))
        {
            if (player.CanAddItem("WheatDrop"))
            {
                player.inventory.AddSlot("WheatDrop", wheat.dropSprite);
                Destroy(collision.gameObject);
            }
            else
            {
                ChestUI chestScript = ChestPanel.GetComponent<ChestUI>();
                chestScript.Append("WheatDrop", wheat.dropSprite);
                Destroy(collision.gameObject);
            }
        }
        if (collision.name.StartsWith("DropTomato"))
        {
            if (player.CanAddItem("TomatoDrop"))
            {
                player.inventory.AddSlot("TomatoDrop", tomato.dropSprite);
                Destroy(collision.gameObject);
            }
            else
            {
                ChestUI chestScript = ChestPanel.GetComponent<ChestUI>();
                chestScript.Append("TomatoDrop", tomato.dropSprite);
                Destroy(collision.gameObject);
            }
        }
    }
    public void SaveData(ref GameData data)
    {
        GameObject[] prefabsWheat = GameObject.FindGameObjectsWithTag("Wheat");
        data.wheats.Clear();
        foreach (GameObject pref in prefabsWheat)
        {
            //Wheat wheat = new Wheat();
            Wheat wheat=pref.GetComponent<Wheat>();
            //PlantPref plant = new PlantPref(wheat.growthDegree,wheat.position);
            data.wheats.Add(new PlantPref(wheat.growthDegree, wheat.transform.position));
        }
        GameObject[] prefabsTomato = GameObject.FindGameObjectsWithTag("Tomato");
        data.tomatos.Clear();
        foreach (GameObject pref in prefabsTomato)
        {
            //Wheat wheat = new Wheat();
           Tomato tomato = pref.GetComponent<Tomato>();
            //PlantPref plant = new PlantPref(wheat.growthDegree,wheat.position);
            data.tomatos.Add(new PlantPref(tomato.growthDegree, tomato.transform.position));
        }
    }
    public void LoadData(GameData data)
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name=="SampleScene")
        {
            Debug.Log("Sono in load");
            if (data.wheats.Count > 0)
            {
                for (int i = 0; i < data.wheats.Count; i++)
                {
                    //wheats.Add(data.wheats[i]);
                    //Debug.Log(data.wheats[i].growthDegree+ "--" + data.wheats[i].position.ToString());
                    wheatPrefab.GetComponent<Wheat>().growthDegree = data.wheats[i].growthDegree;
                    Instantiate(wheatPrefab, data.wheats[i].position, Quaternion.identity);
                }
            }
            if (data.tomatos.Count > 0)
            {
                for (int i = 0; i < data.tomatos.Count; i++)
                {
                    //wheats.Add(data.wheats[i]);
                    //Debug.Log(data.wheats[i].growthDegree+ "--" + data.wheats[i].position.ToString());
                    tomatoPrefab.GetComponent<Tomato>().growthDegree = data.tomatos[i].growthDegree;
                    Instantiate(tomatoPrefab, data.tomatos[i].position, Quaternion.identity);
                }
            }
        }
        
    }
}
