using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool isOpen = false;
    public Sprite OpenedChest;
    public Sprite ClosedChest;
    public SpriteRenderer spriteRenderer;
    public ChestUI ChestPanel;
    public GameObject chestUIPanel;
    [SerializeField] private LayerMask playerLayer;
    private Transform parent;
    // Start is called before the first frame update
    /*void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ClosedChest;
        /*GameObject canvas = GameObject.FindWithTag("InventoryCanvas");
        chestUIPanel = canvas.transform.Find("ChestUI").gameObject;
        chestUIPanel = GameObject.Find("ChestUIPanel");
        ChestPanel=chestUIPanel.GetComponent<ChestUI>();
    }*/
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ClosedChest;
        parent = GameObject.FindGameObjectWithTag("InventoryCanvas").transform;

    }

// Update is called once per frame
    void Update()
    {
        /*GameObject canvas = GameObject.FindWithTag("InventoryCanvas"); // Assicurati di assegnare il tag
        if (canvas != null)
        {
            Transform chestTransform = canvas.transform.Find("chestUIPanel");
            if (chestTransform != null)
            {
                chestUIPanel = chestTransform.gameObject;
                ChestPanel = chestUIPanel.GetComponent<ChestUI>();
            }
        }*/
        
        FindChildWithTag(parent, "ChestUI");
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 1f, playerLayer);
            if (players.Length > 0)
            {
                Open();
            }
        }
        
    }
    public void FindChildWithTag(Transform parent, string tag)
    {
        
        foreach (Transform child in parent)
        {

            // Ricorsione per cercare anche tra i figli annidati
            FindChildWithTag(child, tag);
        }
    }
    public void Open()
    {
        
        if (!isOpen)
        {
            spriteRenderer.sprite = OpenedChest;
            isOpen = true;
            ChestUI chestUI = GetChestUI();

            if (chestUI == null)
                return;

            chestUI.ToggleChest();
        }
        else
        {
            ChestUI chestUI = GetChestUI();

            if (chestUI == null)
                return;

            chestUI.ToggleChest();
            isOpen = false;
            StartCoroutine(Timer());
        }
    }
    private IEnumerator Timer()
    {
        // Aspetta il tempo di respawn
        yield return new WaitForSeconds(0.5f);
        Close();
    }
    public void Close()
    {
        spriteRenderer.sprite = ClosedChest;
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.E))
            {
                Open();
            }
        }
    }
    private ChestUI GetChestUI()
    {
        ChestUI chestUI = FindObjectOfType<ChestUI>(true);

        if (chestUI == null)
        {
            Debug.LogError("ChestUI non trovata. Controlla che il Canvas della cesta esista nella scena.");
        }

        return chestUI;
    }
}
