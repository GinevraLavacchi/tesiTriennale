using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static RecipesManagerf;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TestSlot : MonoBehaviour
{
    [SerializeField] public GameObject prefabSlot;
    [SerializeField] public Transform positionTable1;
    [SerializeField] public Transform positionTable2;
    [SerializeField] public Transform positionTable3;
    [SerializeField] public Transform positionTable4;
    public string type;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public WorkBench_UI work;
    public Image image;
    public GameObject workbench;
    public GameObject chestPanel;

    private void Start()
    {
        workbench = GameObject.FindGameObjectWithTag("Desk");
        // chestPanel = GameObject.FindWithTag("ChestUI");
        GameObject chest = GameObject.FindGameObjectWithTag("Chest");
        Chest chest1=chest.GetComponent<Chest>();
        chestPanel = chest1.chestUIPanel;
        Transform childWithTag = FindChildWithTag(transform, "objWorkBench");

        if (childWithTag != null)
        {
            // Recupera il componente Image
             image = childWithTag.GetComponent<Image>();
        }
        else
        {
            Debug.LogWarning("Nessun figlio con tag trovato.");
        }
    }
    // Funzione ricorsiva per trovare un figlio con un tag specifico
    Transform FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child;
            }
            Transform result = FindChildWithTag(child, tag);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
    public void Append()
    {
        if (positionTable1.childCount > 0)
        {
            Transform child = positionTable1.GetChild(0);
            Destroy(child.gameObject);
        }

        Image prefabImage = prefabSlot.GetComponentInChildren<Image>();
        RectTransform rect = prefabImage.GetComponent<RectTransform>();

        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 1);
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        prefabImage.transform.localScale = new Vector2(0.8f, 0.8f);
        prefabImage.sprite = image.sprite;

        WorkBench w = workbench.GetComponent<WorkBench>();
        w.nameRicetta = type;

        GameObject newSlot = Instantiate(prefabSlot, positionTable1.transform, false);

        TextMeshProUGUI counter = newSlot.GetComponentInChildren<TextMeshProUGUI>();

        if (counter != null)
        {
            counter.text = "";
        }
    }
    /* public void Append()
     {
         if(positionTable1.childCount > 0)
         {
             Transform child=positionTable1.GetChild(0);
             Destroy(child.gameObject);
             // Imposta l'ancoraggio per lo stretch
             prefabSlot.GetComponentInChildren<Image>().GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
             prefabSlot.GetComponentInChildren<Image>().GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);

             // Resetta gli offset
             prefabSlot.GetComponentInChildren<Image>().GetComponent<RectTransform>().offsetMin = Vector2.zero; // Sinistra e Giù
             prefabSlot.GetComponentInChildren<Image>().GetComponent<RectTransform>().offsetMax = Vector2.zero; // Destra e Su
             prefabSlot.GetComponentInChildren<Image>().transform.localScale = new Vector2(0.8f, 0.8f);
             prefabSlot.GetComponentInChildren<Image>().sprite = image.sprite;
             WorkBench w =workbench.GetComponent<WorkBench>();
             w.nameRicetta = type;

             Instantiate(prefabSlot, positionTable1.transform, false);

         }
         else
         {
             // Imposta l'ancoraggio per lo stretch
             prefabSlot.GetComponentInChildren<Image>().GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
             prefabSlot.GetComponentInChildren<Image>().GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);

             // Resetta gli offset
             prefabSlot.GetComponentInChildren<Image>().GetComponent<RectTransform>().offsetMin = Vector2.zero; // Sinistra e Giù
             prefabSlot.GetComponentInChildren<Image>().GetComponent<RectTransform>().offsetMax = Vector2.zero; // Destra e Su
             prefabSlot.GetComponentInChildren<Image>().transform.localScale = new Vector2(0.8f, 0.8f);
             prefabSlot.GetComponentInChildren<Image>().sprite = image.sprite;
             WorkBench w = workbench.GetComponent<WorkBench>();
             w.nameRicetta = type;
             Instantiate(prefabSlot, positionTable1.transform, false);
         }

     }*/
    public void AppendItem()
    {
        prefabSlot.GetComponentInChildren<Image>().sprite = image.sprite;
        int.TryParse(quantityText.text, out int quantity);
        WorkBench w = workbench.GetComponent<WorkBench>();
        if (positionTable2.childCount > 0)
            {
                Image img2 = positionTable2.GetChild(0).gameObject.GetComponent<Image>();
                /*Debug.Log(img2.sprite.ToString());
                Debug.Log(image.sprite.ToString());*/

                if (img2.sprite.ToString() == image.sprite.ToString())
                {
                    TextMeshProUGUI counter = positionTable2.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                    int.TryParse(counter.text, out int numero);
                    if(numero<quantity)
                    {
                        numero++;
                        counter.text = numero.ToString();
                        w.SetArray(type, numero);
                    }
                    
                }
                else
                {
                    if (positionTable3.childCount > 0)
                    {
                        Image img3 = positionTable3.GetChild(0).gameObject.GetComponent<Image>();
                        if (img3.sprite.ToString() == image.sprite.ToString())
                        {
                            TextMeshProUGUI counter = positionTable3.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                            int.TryParse(counter.text, out int numero);
                            if (numero < quantity)
                            {
                                numero++;
                                counter.text = numero.ToString();
                                w.SetArray(type, numero);
                            }
                        }
                        else
                        {
                            if (positionTable4.childCount > 0)
                            {
                                Image img4 = positionTable4.GetChild(0).gameObject.GetComponent<Image>();
                                if (img4.sprite.ToString() == image.sprite.ToString())
                                {
                                    TextMeshProUGUI counter = positionTable4.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                                    int.TryParse(counter.text, out int numero);
                                    if (numero < quantity)
                                    {
                                        numero++;
                                        counter.text = numero.ToString();
                                        w.SetArray(type, numero);
                                    }
                                }
                                else
                                {
                                    Debug.LogError("ATTENZIONE SLOT PIENI");

                                }
                            }
                            else
                            {
                                Instantiate(prefabSlot, positionTable4.transform, false);
                                TextMeshProUGUI counter = positionTable4.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                                counter.text = "1";
                                w.SetArray(type, 1);
                        }
                    }

                    }
                    else
                    {
                        Instantiate(prefabSlot, positionTable3.transform, false);
                        TextMeshProUGUI counter = positionTable3.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                        counter.text = "1";
                        w.SetArray(type, 1);
                    }
                }
                //devo controllare se la sprite che dovrei mettere è uguale a quella che ho già, se è così devo cambiare il text +1

            }
            else
            {
                Instantiate(prefabSlot, positionTable2.transform, false);
                TextMeshProUGUI counter = positionTable2.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                counter.text = "1";
                w.SetArray(type, 1);
            }
        
        
    }
    public void SetItem(Inventory.Slot slot)
    {
        if (slot != null)
        {
            type = slot.type.ToString();
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.count.ToString();

        }
    }

    public void SetTool(string tool, int life)
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
        type = "";
    }
    public void OnSlotClicked()
    {
        GameObject audioMan = GameObject.FindGameObjectWithTag("AudioManager");
        AudioManager au=audioMan.GetComponent<AudioManager>();
        au.PlaySoundEffect(au.slotSound);
        if (chestPanel.activeSelf)
        {
            if (type != "axe" && type != "pickaxe" && type != "sword" && type != "hoe"&&type!="NONE")
            {
                ChestUI chestUI = chestPanel.GetComponent<ChestUI>();
                if (chestUI != null)
                {
                    chestUI.Append(type,itemIcon.sprite);
                    GameObject player = GameObject.FindWithTag("Player");
                    Player player1 = player.GetComponent<Player>();
                    player1.inventory.FindItemToRemove(type, 1);
                }
            }
            
        }
        else
        {
            GameObject player = GameObject.FindWithTag("Player");
            Debug.Log("Slot Cliccato:" + type);
            if (player != null)
            {
                //axe, pickaxe, sword, hoe
                if (type == "axe" || type == "pickaxe" || type == "sword" || type == "hoe")
                {
                    Player script = player.GetComponent<Player>();
                    script.tool = type;
                }
                if (type.EndsWith("Fertilizer"))
                {
                    PlayerHarvest plharverst = player.GetComponent<PlayerHarvest>();
                    Player pl = player.GetComponent<Player>();
                    if (plharverst.FertilizePlant(type))
                    {
                        pl.inventory.FindItemToRemove(type, 1);

                    }
                }
                if (type.EndsWith("Seed"))
                {
                    PlayerHarvest plhar = player.GetComponent<PlayerHarvest>();
                    if(plhar.Plant(type))
                    {
                        Player player1 = player.GetComponent<Player>();
                        player1.inventory.FindItemToRemove(type, 1);
                    }
                }
                if(type== "Apple")
                {
                    PlayerHealth playerHealth= player.GetComponent<PlayerHealth>();
                   if(!playerHealth.isMaxHealth())
                    {
                        playerHealth.UpdateHealth(0.5f);

                        Player player1 = player.GetComponent<Player>();
                        player1.inventory.FindItemToRemove(type, 1);
                    }
                        
                }
                if (type == "bread")
                {
                    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                    if (!playerHealth.isMaxHealth())
                    {
                        playerHealth.UpdateHealth(2.0f);

                        Player player1 = player.GetComponent<Player>();
                        player1.inventory.FindItemToRemove(type, 1);
                    }

                }
            }
        }
    }
}
