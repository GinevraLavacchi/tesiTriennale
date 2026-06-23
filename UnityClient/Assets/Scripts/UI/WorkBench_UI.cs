using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static RecipesManagerf;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEngine.Rendering.DebugUI;

public class WorkBench_UI : MonoBehaviour
{
    
    public GameObject workbenchPanel;
    public Player player;
    public Inventory_UI inventory_UI;
    //public List<Slot_UI> slots = new List<Slot_UI>();
    //public List<Slot_UI> slotsTool = new List<Slot_UI>();
    public List<TestSlot> slots = new List<TestSlot>();
    public List<TestSlot> slotsTool = new List<TestSlot>();
    public string toolToForge;
    public Image img1;
    public Image img2;
    public Image img3;
    public Image img4;
    public Sprite axe;
    public Sprite pickaxe;
    public Sprite sword;
    public Sprite hoe;
    public Sprite wood;
    public Sprite slimeBall;
    public Sprite apple;
    public Sprite rock;
    public GameObject prefabSlot;
    public Transform table;
    private void Start()
    {
        GameObject pl = GameObject.FindGameObjectWithTag("Player");
        player=pl.GetComponent<Player>();
        img1.sprite = null;
        img1.color = new Color(1, 1, 1, 0);
        img2.sprite = null;
        img2.color = new Color(1, 1, 1, 0);
        img3.sprite = null;
        img3.color = new Color(1, 1, 1, 0);
        img4.sprite = null;
        img4.color = new Color(1, 1, 1, 0);
        workbenchPanel.SetActive(false);
        gameObject.transform.SetAsLastSibling();
    }
    public void Update()
    {
        for (int i=0; i< slotsTool.Count; i++)
        {
            slotsTool[i].SetTool(player.toolList[i].type, player.toolList[i].life);
        }
        for (int i =0; i < slots.Count; i++)
        {
            if (player.inventory.slots[i].type != "NONE")
            {
                slots[i].SetItem(player.inventory.slots[i]);
            }
            else
            {
                slots[i].SetEmpty();
            }
        }
        
    }
    public void ToggleWorkbench()
    {
        /*GameObject audiomen = GameObject.FindGameObjectWithTag("AudioManager");
        AudioManager aum = audiomen.GetComponent<AudioManager>();
        aum.PlaySoundEffect(aum.workbenchSound);*/
        if (!workbenchPanel.activeSelf)
        {
            workbenchPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            workbenchPanel.SetActive(false);
            img1.sprite = null;
            img1.color = new Color(1, 1, 1, 0);
            img2.sprite = null;
            img2.color = new Color(1, 1, 1, 0);
            img3.sprite = null;
            img3.color = new Color(1, 1, 1, 0);
            img4.sprite = null;
            img4.color = new Color(1, 1, 1, 0);
            Time.timeScale = 1f;
        }
 
    }
   
    public void SetImg(string type)
    {
        Sprite spr= apple;
        Debug.Log(type);
        if (type == "Wood") { spr = wood; } else if (type == "Apple") { spr = apple; } else if (type == "Rock") { spr = rock; } else if (type == "SlimeBall") { spr = slimeBall; }
        if (img2.sprite!=null)
        {
            if(img3.sprite!=null)
            {
                if(img4.sprite==null)
                {
                    
                    img4.sprite = spr;
                    img4.color = new Color(1, 1, 1, 1);
                    img4.SetNativeSize();
                    img4.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                img3.sprite = spr;
                img3.color = new Color(1, 1, 1, 1);
                img3.SetNativeSize();
                img3.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
        }
        else
        {
            img2.sprite = spr;
            img2.color = new Color(1, 1, 1, 1);
            img2.SetNativeSize();
            img2.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }
    public void CleanTable()
    {
        Transform panel1 = table.GetChild(0);
        Transform panel2 = table.GetChild(1);
        Transform panel3 = table.GetChild(2);
        Transform panel4 = table.GetChild(3);

        if (panel1.childCount > 0)
        {
            Destroy(panel1.GetChild(0).gameObject);
        }
        if (panel2.childCount > 0)
        {
            Destroy(panel2.GetChild(0).gameObject);
        }
        if (panel3.childCount > 0)
        {
            Destroy(panel3.GetChild(0).gameObject);
        }
        if (panel4.childCount > 0)
        {
            Destroy(panel4.GetChild(0).gameObject);
        }
        GameObject work = GameObject.FindGameObjectWithTag("Desk");
        WorkBench workscript= work.GetComponent<WorkBench>();
        workscript.ingredients.Clear();
        workscript.nameRicetta = "";
    }
    /*bool IsInstanceOfPrefab(GameObject prefab, GameObject obj)
    {
        // Ottieni il prefab originale dell'oggetto
        GameObject prefabInstance = PrefabUtility.GetPrefabAssetType(obj) != PrefabAssetType.NotAPrefab ?
                                    PrefabUtility.GetPrefabParent(obj) as GameObject : null;

        return prefabInstance != null && prefabInstance == prefab;
    }*/
}
