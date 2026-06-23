using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, DataPersistanceInterface
{
    public class Tool
    {
        public string type;
        public int life = 500;
        public Tool(string type, int life)
        {
            this.type = type;
            this.life = life;
        }

        public void SetType(string toolType)
        {
            type = toolType;
        }
        public bool IsUsable()
        {
            if (life <= 0)
            {
                return false;
            }
            return true;
        }
        public void SetLife(int life)
        {
            this.life = life;
        }
        public void Used(int life)
        {
            this.life = this.life - life;
        }
    }
    [SerializeField] public Inventory inventory;
    [SerializeField] public List<Tool> toolList = new List<Tool>(4);
    public string tool="axe";
    public Sprite axe;
    public Sprite pickaxe;
    public Sprite sword;
    public Sprite hoe;
    [SerializeField] public Sprite wheatSeedIcon;
    [SerializeField] public Sprite tomatoSeedIcon;
    [SerializeField] public Sprite wheatDrop;
    public Image img;
    private int inventorycount=6;
    [SerializeField] private Texture2D mouseArrow;
    public static Player instance; // Singleton
    public void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        inventory = new Inventory(inventorycount);

        toolList.Clear();
        toolList.Add(new Tool("axe", 50));
        toolList.Add(new Tool("pickaxe", 50));
        toolList.Add(new Tool("sword", 50));
        toolList.Add(new Tool("hoe", 50));

        inventory.AddSlot("wheatSeed", wheatSeedIcon);
        inventory.AddSlot("tomatoSeed", tomatoSeedIcon);
        inventory.AddSlot("WheatDrop", wheatDrop);
    }
    private void Start()
    {
        Cursor.SetCursor(mouseArrow, Vector2.zero, CursorMode.ForceSoftware);
    }
        public void Update()
    {
        if (img == null || !img.gameObject.activeInHierarchy)
            return;
        // Controlla la pressione dei tasti numerici
        if (Input.GetKeyDown(KeyCode.Alpha1)) { tool="axe"; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { tool = "pickaxe"; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { tool = "sword"; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { tool = "hoe"; }
        if (tool=="axe")
        {
            img.sprite =axe;
        }
        else if (tool == "pickaxe")
        {
            img.sprite = pickaxe;
        }
        else if (tool == "sword")
        {
            img.sprite = sword;
        }
        else if (tool == "hoe")
        {
            img.sprite = hoe;
        }
        img.SetNativeSize();
        img.transform.localScale = new Vector3(5f, 5f, 5f);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MenuScene")
        {
            SetChildrenActive(false);
        }
        else if (scene.name == "SampleScene")
        {
            SetChildrenActive(true);
        }
    }

    private void SetChildrenActive(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public bool FindToolToRepair(string name)
    {
        foreach (Tool tool in toolList)
        {
            if (tool.type == name)
            {
                tool.SetLife(100);
                return true;
            }
        }

        return false;
    }
    public void ToolUsed()
    {
        foreach (Tool tool1 in toolList)
        {
            if (tool1.type == tool)
            {
                tool1.Used(10);
            }
        }
    }
    public bool CanAddItem(string type)
    {
        if (!inventory.isFull(type)) { return true; }
        return false;

    }
    private Sprite GetIconFromType(string type)
    {
        if (type == "wheatSeed")
            return wheatSeedIcon;

        if (type == "tomatoSeed")
            return tomatoSeedIcon;

        return null;
    }
    /* public List<InventorySlotData> GetSaveData()
     {
         List<InventorySlotData> saveSlots = new List<InventorySlotData>();

         foreach (Inventory.Slot slot in inventory.slots)
         {
             InventorySlotData data = new InventorySlotData();

             data.type = slot.type;
             data.count = slot.count;

             saveSlots.Add(data);
         }

         return saveSlots;
     }

     public void LoadSaveData(List<InventorySlotData> saveSlots)
     {
         inventory.slots.Clear();

         foreach (InventorySlotData data in saveSlots)
         {
             Inventory.Slot slot = new Inventory.Slot();

             slot.type = data.type;
             slot.count = data.count;

             if (data.type != "NONE")
                 slot.icon = ItemIconDatabase.Instance.GetIcon(data.type);
             else
                 slot.icon = null;

             inventory.slots.Add(slot);
         }

         Inventory_UI inventoryUI = FindObjectOfType<Inventory_UI>();

         if (inventoryUI != null)
         {
             inventoryUI.Setup();
         }
     }*/
    public void SaveData(ref GameData data)
    {
        Debug.Log("PLAYER SAVE DATA | inventory.slots.Count=" + inventory.slots.Count);

        data.slots.Clear();

        foreach (Inventory.Slot slot in inventory.slots)
        {
            Debug.Log("SAVE SLOT | type=" + slot.type + " count=" + slot.count);
            data.slots.Add(slot);
        }

        Debug.Log("PLAYER SAVE END | data.slots.Count=" + data.slots.Count);
    }

    public void LoadData(GameData data)
    {
        Debug.Log("PLAYER LOAD DATA | data.slots.Count=" + data.slots.Count);

        inventory.slots.Clear();

        foreach (Inventory.Slot slot in data.slots)
        {
            Debug.Log("LOAD SLOT | type=" + slot.type + " count=" + slot.count);
            inventory.slots.Add(slot);
        }

        Debug.Log("PLAYER LOAD END | inventory.slots.Count=" + inventory.slots.Count);
    }
}
