using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using System;
using static Inventory;
using UnityEngine.SceneManagement;

public class ChestUI : MonoBehaviour, DataPersistanceInterface
{
    [SerializeField] public GameObject slotsobj;
    [SerializeField] List<ChestSlot> slots = new List<ChestSlot>();
    //public GameObject chestPanel;
    public GameObject inventoryPanel;
    [SerializeField] private Button button;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        /*int childCount = slotsobj.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject figlio= slotsobj.transform.GetChild(i).gameObject;
            if(figlio.GetComponent<ChestSlot>()!=null )
            {
                slots.Add(figlio.GetComponent<ChestSlot>());
                //slots[i].SetEmpty();
            }
        }*/

    }
    public void Awake()
    {
        int childCount = slotsobj.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject figlio = slotsobj.transform.GetChild(i).gameObject;
            if (figlio.GetComponent<ChestSlot>() != null)
            {
                slots.Add(figlio.GetComponent<ChestSlot>());
                //slots[i].SetEmpty();
            }
        }
    }
    public void ToggleChest()
    {
        GameObject audioMan = GameObject.FindGameObjectWithTag("AudioManager");
        AudioManager au = audioMan.GetComponent<AudioManager>();
        au.PlaySoundEffect(au.chestSound);
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            Inventory_UI invUI = inventoryPanel.GetComponent<Inventory_UI>();
            invUI.ToggleInventory();
            button.interactable = false;
            Time.timeScale = 0f;
           /* Chest chest=GameObject.FindWithTag("Chest").GetComponent<Chest>();
            chest.Open();*/
        }
        else
        {
            gameObject.SetActive(false);
            Inventory_UI invUI = inventoryPanel.GetComponent<Inventory_UI>();
            invUI.ToggleInventory();
            button.interactable = true;
            Time.timeScale = 1f;
            /*Chest chest = GameObject.FindWithTag("Chest").GetComponent<Chest>();
            chest.Close();*/
        }

    }
    public bool Append(string type, Sprite img, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            bool added = false;

            foreach (ChestSlot slot in slots)
            {
                if (slot.type == type)
                {
                    string str = slot.quantityText.text;

                    if (int.TryParse(str, out int number))
                    {
                        number++;
                        slot.quantityText.text = number.ToString();
                        added = true;
                        break;
                    }
                }
            }

            if (!added)
            {
                foreach (ChestSlot slot in slots)
                {
                    if (slot.type == "")
                    {
                        slot.type = type;
                        slot.quantityText.text = "1";
                        slot.itemIcon.sprite = img;
                        slot.itemIcon.color = new Color(1, 1, 1, 1);
                        added = true;
                        break;
                    }
                }
            }

            if (!added)
            {
                return false;
            }
        }

        return true;
    }
    public void SaveData(ref GameData data)
    {
        data.chest.Clear();

        foreach (ChestSlot slot in slots)
        {
            if (slot == null) continue;
            if (slot.itemIcon == null) continue;
            if (slot.quantityText == null) continue;

            if (int.TryParse(slot.quantityText.text, out int number) && number > 0)
            {
                data.chest.Add(new Inventory.Slot(
                    slot.type,
                    number,
                    slot.itemIcon.sprite
                ));
            }
        }
    }
    public void LoadData(GameData data)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetEmpty();
        }

        for (int i = 0; i < data.chest.Count && i < slots.Count; i++)
        {
            slots[i].quantityText.text = data.chest[i].count.ToString();
            slots[i].itemIcon.sprite = data.chest[i].icon;
            slots[i].type = data.chest[i].type;
        }
    }
    /* public void SaveData(ref GameData data)
     {
         data.chest.Clear();
         foreach (ChestSlot slot in slots)
         {
             if (int.TryParse(slot.quantityText.text, out int number))
             {
                 data.chest.Add(new Inventory.Slot(slot.type, number, slot.itemIcon.sprite));
             }
         }

     }

     public void LoadData(GameData data)
     {
         if (data.chest.Count > 0)
         {
             int val = slots.Count - data.chest.Count;
             for (int i = 0; i < data.chest.Count; i++)
             {
                 slots[i].quantityText.text = data.chest[i].count.ToString();
                 slots[i].itemIcon.sprite = data.chest[i].icon;
                 slots[i].type = data.chest[i].type;

             }
             for (int i = data.chest.Count; i < val; i++)
             {
                 slots[i].SetEmpty();
             }
         }
     }*/
}
