using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerGathering : MonoBehaviour
{
    public LayerMask treeLayer;
    public float gatheringRange = 0.5f;  // Raggio di interazione con l'albero
    [SerializeField] public int wood=0;
    [SerializeField] public int apple = 0;
    [SerializeField] public int slimeDrop = 0;
    [SerializeField] public int whatFertilizer = 0;
    [SerializeField] public int tomatoFertilizer = 0;
    public Player player;
    [Header("Sprite per inventory")]
    public Sprite woodIcon;
    public Sprite appleIcon;
    public Sprite slimeIcon;
    public GameObject ChestPanel;

    public int ironStone;
    public int copperStone;
    public Sprite ironIcon;
    public Sprite copperIcon;
    public void Awake()
    {
        player = GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        int life=0;
        PlayerAttack plAttack = gameObject.GetComponent<PlayerAttack>();
        // Controlla se il giocatore preme il tasto di raccolta
        for (int i = 0; i < player.toolList.Count; i++)
        {
            if(player.toolList[i].type == player.tool)
            {
                 life=player.toolList[i].life;
            }
        }
        if (Input.GetMouseButtonDown(0)&&player.tool=="axe"&&life>0)
        {

            // Cerca gli alberi vicini al personaggio all'interno del raggio
            Collider2D[] trees = Physics2D.OverlapCircleAll(transform.position, gatheringRange, treeLayer);

            // Se ci sono alberi nell'area, abbatti il primo che trovi
            foreach (Collider2D treeCollider in trees)
            {
                Tree treeScript = treeCollider.GetComponent<Tree>();

                if (treeScript != null)
                {
                    // Chiede all'albero di abbatterlo
                    treeScript.CutDown();
                    player.ToolUsed();
                    break;  // Interrompi il loop dopo aver abbattuto un albero
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && player.tool == "pickaxe" && life > 0)
        {

            plAttack.DetectStones();
        }
        if(Input.GetMouseButtonDown(0) && player.tool == "sword" && life > 0)
        {

            GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
            AudioManager aum = audiomen.GetComponent<AudioManager>();
            aum.PlaySoundEffect(aum.attackSound);
            plAttack.Attack();
        }
    }
    // Disegna un cerchio nell'editor per mostrare il raggio di raccolta
    private void OnDrawGizmos()
    {
        // Imposta il colore del cerchio
        Gizmos.color = Color.green;

        // Disegna il cerchio attorno alla posizione del giocatore
        Gizmos.DrawWireSphere(transform.position, gatheringRange);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith("Wood"))
        {

            GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
            AudioManager aum = audiomen.GetComponent<AudioManager>();
            aum.PlaySoundEffect(aum.collisionCollectSound);
            if (player.CanAddItem("Wood"))
            {
                wood++;
                player.inventory.AddSlot("Wood", woodIcon);
                Destroy(collision.gameObject);
            }
            else
            {
                wood++;
                ChestUI chestScript = ChestPanel.GetComponent<ChestUI>();
                chestScript.Append("Wood", woodIcon);
                Destroy(collision.gameObject);
            }
        }
        if (collision.name.StartsWith("Apple"))
        {

            GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
            AudioManager aum = audiomen.GetComponent<AudioManager>();
            aum.PlaySoundEffect(aum.collisionCollectSound);
            if (player.CanAddItem("Apple"))
            {
                apple++;
                player.inventory.AddSlot("Apple", appleIcon);
                Destroy(collision.gameObject);
            }
            else
            {
                apple++;
                ChestUI chestScript = ChestPanel.GetComponent<ChestUI>();
                chestScript.Append("Apple", appleIcon); 
                Destroy(collision.gameObject);
            }

        }
        if (collision.name.StartsWith("Slime")) 
        {

            GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
            AudioManager aum = audiomen.GetComponent<AudioManager>();
            aum.PlaySoundEffect(aum.collisionCollectSound);
            if (player.CanAddItem("Slime"))
            {
                slimeDrop++;
                player.inventory.AddSlot("SlimeDrop", slimeIcon);
                Destroy(collision.gameObject);
            }
            else
            {
                slimeDrop++;
                ChestUI chestScript = ChestPanel.GetComponent<ChestUI>();
                chestScript.Append("SlimeDrop", slimeIcon);
                Destroy(collision.gameObject);
            }

        }
        if(collision.name.StartsWith("Iron"))
        {

            GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
            AudioManager aum = audiomen.GetComponent<AudioManager>();
            aum.PlaySoundEffect(aum.collisionCollectSound);
            if (player.CanAddItem("Iron"))
            {
                ironStone++;
                player.inventory.AddSlot("Iron", ironIcon);
                Destroy(collision.gameObject);
            }
            else
            {
                ironStone++;
                ChestUI chestScript = ChestPanel.GetComponent<ChestUI>();
                chestScript.Append("Iron", ironIcon);
                Destroy(collision.gameObject);
            }
            
        }
        if (collision.name.StartsWith("Copper"))
        {

            GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
            AudioManager aum = audiomen.GetComponent<AudioManager>();
            aum.PlaySoundEffect(aum.collisionCollectSound);
            if (player.CanAddItem("Copper"))
            {
                copperStone++;
                player.inventory.AddSlot("Copper", copperIcon);
                Destroy(collision.gameObject);
            }
            else
            {
                copperStone++;
                ChestUI chestScript = ChestPanel.GetComponent<ChestUI>();
                chestScript.Append("Copper", copperIcon);
                Destroy(collision.gameObject);
            }
        }
    }
}
