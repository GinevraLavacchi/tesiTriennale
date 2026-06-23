using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesManagerf : MonoBehaviour
{
    [System.Serializable]
    public class Ingrediente
    {
        public string nome;
        public int quantita;
        public Ingrediente(string nome, int quantita)
        {
            this.nome = nome;
            this.quantita = quantita;
        }
    }

    [System.Serializable]
    public class Ricetta
    {
        public string nome;
        public List<Ingrediente> ingredienti = new List<Ingrediente>();

        public Ricetta(string nome, List<Ingrediente> ingredienti)
        {
            this.nome = nome;
            this.ingredienti = ingredienti;
        }

    }

    [System.Serializable]
    public class Ricettario
    {
        public List<Ricetta> ricette;
    }
    public Sprite bread;
    public Sprite wheatSeed;
    public Sprite tomatoSeed;
    public Sprite tomatoFert;
    public Sprite wheatFert;
    public Ricettario ricettario;
    public GameObject chest;
    // Start is called before the first frame update
    void Start()
    {
        CaricaRicette();
        //StampaRicette();
    }

    void CaricaRicette()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Ricette"); // Legge il file JSON da Resources
        if (jsonFile != null)
        {
            //Debug.Log($"Contenuto del JSON caricato:\n{jsonFile.text}");

            ricettario = JsonUtility.FromJson<Ricettario>(jsonFile.text);

            
        }
        else
        {
            Debug.LogError("File JSON non trovato!");
        }
    }

    /*void StampaRicette()
    {
        foreach (var ricetta in ricettario.ricette)
        {
            Debug.Log($"Ricetta: {ricetta.nome}");
            foreach (var ingrediente in ricetta.ingredienti)
            {
                Debug.Log($"- {ingrediente.nome}: {ingrediente.quantita}");
            }
        }
    }*/
    public bool IsSameReepi(Ricetta ricetta)
    {
        List<Ingrediente> ingrs= new List<Ingrediente>();
        int counter = 0;
        if(ricetta.nome!="create")
        {
            foreach (Ricetta ricetta1 in ricettario.ricette)
            {
                if (ricetta1.nome == ricetta.nome)
                {
                    foreach (Ingrediente ingrediente in ricetta.ingredienti)
                    {
                        for (int i = 0; i < ricetta1.ingredienti.Count; i++)
                        {
                            if (ingrediente.nome == ricetta1.ingredienti[i].nome && ingrediente.quantita >= ricetta1.ingredienti[i].quantita && ricetta.ingredienti.Count == ricetta1.ingredienti.Count)
                            {
                                counter++;
                                ingrs.Add(ricetta1.ingredienti[i]);
                            }
                        }
                    }
                    if (counter == ricetta.ingredienti.Count)
                    {
                        GameObject pl = GameObject.FindGameObjectWithTag("Player");
                        Player player = pl.GetComponent<Player>();
                        for (int i = 0; i < ricetta.ingredienti.Count; i++)
                        {
                            player.inventory.FindItemToRemove(ingrs[i].nome, ingrs[i].quantita);
                        }
                        if (ricetta.nome == "axe" || ricetta.nome == "pickaxe" || ricetta.nome == "hoe" || ricetta.nome == "sword")
                        {
                            player.FindToolToRepair(ricetta.nome);
                        }
                        else
                        return true;
                    }

                   
                }

            }
        }
        else //devo creare oggetto
        { 
            foreach (Ricetta ricetta1 in ricettario.ricette)
            {
                if(ricetta.ingredienti.Count == ricetta1.ingredienti.Count)
                {
                    foreach (Ingrediente ingrediente in ricetta.ingredienti)
                    {
                        for (int i = 0; i < ricetta1.ingredienti.Count; i++)
                        {

                            if (ingrediente.nome == ricetta1.ingredienti[i].nome && ingrediente.quantita >= ricetta1.ingredienti[i].quantita)
                            {
                                counter++;
                                //Debug.Log(ingrediente.nome+"=="+ ricetta1.ingredienti[i].nome +"-->"+ counter);
                                ingrs.Add(ricetta1.ingredienti[i]);
                            }
                        }
                    }
                    if (counter == ricetta.ingredienti.Count)
                    {
                        GameObject pl = GameObject.FindGameObjectWithTag("Player");
                        Player player = pl.GetComponent<Player>();
                        for (int i = 0; i < ricetta.ingredienti.Count; i++)
                        {
                            player.inventory.FindItemToRemove(ingrs[i].nome, ingrs[i].quantita);
                        }
                        if (ricetta1.nome == "tomatoFertilizer")
                        {
                            if(player.CanAddItem(ricetta1.nome))
                            {

                                player.inventory.AddSlot(ricetta1.nome, tomatoFert);
                            }
                            else
                            {
                                ChestUI chestScript= chest.GetComponent<ChestUI>();
                                chestScript.Append(ricetta1.nome, tomatoFert);
                            }
                        }
                        else if (ricetta1.nome == "wheatFertilizer")
                        {
                            if (player.CanAddItem(ricetta1.nome))
                            {
                                player.inventory.AddSlot(ricetta1.nome, wheatFert);
                            }
                            else
                            {
                                ChestUI chestScript = chest.GetComponent<ChestUI>();
                                chestScript.Append(ricetta1.nome, wheatFert);
                            }
                        }
                        else if (ricetta1.nome == "bread")
                        {
                            if (player.CanAddItem(ricetta1.nome))
                            {
                                player.inventory.AddSlot(ricetta1.nome, bread);
                            }
                            else
                            {
                                ChestUI chestScript = chest.GetComponent<ChestUI>();
                                chestScript.Append(ricetta1.nome, bread);
                            }
                        }
                        else if (ricetta1.nome == "wheatSeed")
                        {
                            if (player.CanAddItem(ricetta1.nome))
                            {
                                player.inventory.AddSlot(ricetta1.nome, wheatSeed);
                            }
                            else
                            {
                                ChestUI chestScript = chest.GetComponent<ChestUI>();
                                chestScript.Append(ricetta1.nome, wheatSeed);
                            }
                        }
                        else if (ricetta1.nome == "tomatoSeed")
                        {
                            if (player.CanAddItem(ricetta1.nome))
                            {
                                player.inventory.AddSlot(ricetta1.nome, tomatoSeed);
                            }
                            else
                            {
                                ChestUI chestScript = chest.GetComponent<ChestUI>();
                                chestScript.Append(ricetta1.nome, tomatoSeed);
                            }
                        }
                        return true;
                    }
                }
                

            }
        }
        return false;
    }

    public void ApplyRecipeResult(Ricetta ricetta, string action, string resultItem, int resultCount)
    {
        GameObject pl = GameObject.FindGameObjectWithTag("Player");
        Player player = pl.GetComponent<Player>();

        bool success = false;

        if (action == "repair_tool")
        {
            success = player.FindToolToRepair(resultItem);
        }
        else if (action == "craft")
        {
            success = AddCraftedItem(player, resultItem, resultCount);
        }

        if (!success)
        {
            Debug.LogWarning("Operazione non completata: ingredienti non consumati.");
            return;
        }

        foreach (Ingrediente ingrediente in ricetta.ingredienti)
        {
            player.inventory.FindItemToRemove(ingrediente.nome, ingrediente.quantita);
        }
    }

    private bool AddCraftedItem(Player player, string resultItem, int resultCount)
    {
        Sprite sprite = null;

        if (resultItem == "tomatoFertilizer")
            sprite = tomatoFert;
        else if (resultItem == "wheatFertilizer")
            sprite = wheatFert;
        else if (resultItem == "bread")
            sprite = bread;
        else if (resultItem == "wheatSeed")
            sprite = wheatSeed;
        else if (resultItem == "tomatoSeed")
            sprite = tomatoSeed;

        if (sprite == null)
        {
            Debug.LogError("Sprite non trovato per: " + resultItem);
            return false;
        }

        if (player.CanAddItem(resultItem))
        {
            player.inventory.AddSlot(resultItem, sprite, resultCount);
        }
        else
        {
            ChestUI chestScript = chest.GetComponent<ChestUI>();

            if (chestScript == null)
            {
                Debug.LogError("ChestUI non trovata.");
                return false;
            }

            bool addedToChest = chestScript.Append(resultItem, sprite, resultCount);

            if (!addedToChest)
            {
                Debug.LogWarning("Inventario e cesta pieni: impossibile aggiungere " + resultItem);
                return false;
            }
        }

        Inventory_UI inventoryUI = FindObjectOfType<Inventory_UI>();

        if (inventoryUI != null)
        {
            inventoryUI.Setup();
        }

        return true;
    }

}
