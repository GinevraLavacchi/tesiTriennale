using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot_UI : MonoBehaviour
{
    [SerializeField] public GameObject prefabSlot;
    [SerializeField] public Transform positionTable1;
    public string type;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public WorkBench_UI work;
    public string test;

    public void OnSlotClicked()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Debug.Log("Slot Cliccato:" +type);
        if (player != null)
        {
            //axe, pickaxe, sword, hoe
            if(type =="axe"|| type == "pickaxe"|| type == "sword"|| type == "hoe")
            {
                Player script= player.GetComponent<Player>();
                script.tool = type;
            }
        }
        
    }
    public void OnSlotWorkClicked()
    {
        work.SetImg(type);
    }

   /* public void OnToolClicked(string tool)
    {
        Debug.Log(tool);
        if (work != null)
        {
            Debug.Log(tool);
            work.SetToolWork(tool);
        }

    }*/
    public void SetItem(Inventory.Slot slot)
    {
        if (slot != null)
        {
            type=slot.type.ToString();
            itemIcon.sprite=slot.icon;
            itemIcon.color= new Color(1, 1, 1, 1);
            quantityText.text=slot.count.ToString();

        }
    }

    public void SetTool(string tool,int life)
    {
        type = tool;
        itemIcon.color = new Color(1, 1, 1, 1);
        quantityText.text = life.ToString();
    }
    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }
}
