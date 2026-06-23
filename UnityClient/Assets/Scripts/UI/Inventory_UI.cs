using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Inventory;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Player player;
    public List<Slot_UI> slots=new List<Slot_UI>();
    public List<TestSlot> slotstest=new List<TestSlot>();
    private void Start()
    {
        inventoryPanel.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MenuScene")
            return;

        if (player == null)
            player = FindObjectOfType<Player>();

        if (player == null || player.inventory == null)
            return;

        Setup();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
            AudioManager aum = audiomen.GetComponent<AudioManager>();
            aum.PlaySoundEffect(aum.inventorySound);
            ToggleInventory();
        }
    }
    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            Time.timeScale = 0f;
            Setup();
        }
        else
        {
            inventoryPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void Setup()
    {
        if (player.toolList.Count >= 4)
        {
            slotstest[0].SetTool(player.toolList[0].type, player.toolList[0].life);
            slotstest[1].SetTool(player.toolList[1].type, player.toolList[1].life);
            slotstest[2].SetTool(player.toolList[2].type, player.toolList[2].life);
            slotstest[3].SetTool(player.toolList[3].type, player.toolList[3].life);
        }
        else
        {
            Debug.LogWarning("toolList non pronta. Count = " + player.toolList.Count);
            return;
        }

        int maxInventorySlotsUI = slotstest.Count - 4;
        for (int i = 0; i < maxInventorySlotsUI; i++)
        {
            int uiIndex = i + 4;
            if (i >= player.inventory.slots.Count)
            {
                slotstest[uiIndex].SetEmpty();
                continue;
            }

            if (player.inventory.slots[i].type != "NONE")
            {
                slotstest[uiIndex].SetItem(player.inventory.slots[i]);
            }
            else
            {
                slotstest[uiIndex].SetEmpty();
            }
        }
    }
}
