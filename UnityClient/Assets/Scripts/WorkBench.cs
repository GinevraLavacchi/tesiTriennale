using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static RecipesManagerf;

public class WorkBench : MonoBehaviour
{
    /*[System.Serializable]
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
        public List<Ingrediente> ingredienti=new List<Ingrediente>();

        public Ricetta(string nome, List<Ingrediente> ingredienti)
        {
            this.nome = nome;
            this.ingredienti = ingredienti;
        }

    }*/
    
    [SerializeField] public string nameRicetta;
    [SerializeField] public List<RecipesManagerf.Ingrediente> ingredients=new List<RecipesManagerf.Ingrediente>(4);
    public Ricetta ricetta;
    public RecipesManagerf.Ricetta ricetta1;
    public bool isWorking = false;
    public Sprite WorkingBench;
    public Sprite NotWorkingBench;
    public SpriteRenderer spriteRenderer;
    public WorkBench_UI workbenchPanel;
    public RecipesManagerf recipManager;
    [SerializeField] private LayerMask playerLayer;
    public RuleClient ruleClient;

    public void Awake()
    {
        GameObject gm = GameObject.FindWithTag("GameManager");
        recipManager=gm.GetComponent<RecipesManagerf>();

        ruleClient = gm.GetComponent<RuleClient>();
        if (GameObject.FindGameObjectsWithTag("Desk").Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 1f, playerLayer);
            if (players.Length > 0)
            {
                
                Working();
            }
        }
    }
    public void Working()
    {
        if (!isWorking)
        {
            spriteRenderer.sprite = WorkingBench;
            isWorking = true;
            workbenchPanel.ToggleWorkbench();
            //mostrare la possibilità di creare le cose e quando preme x si richiama stopWorking(), per ora metto un timer
            StartCoroutine(Timer());
        }
        else
        {
            isWorking = false;
            workbenchPanel.ToggleWorkbench();
        }
    }
    private IEnumerator Timer()
    {
        // Aspetta il tempo di respawn
        yield return new WaitForSeconds(2f);
        StopWorking();
    }
    public void StopWorking()
    {
        spriteRenderer.sprite = NotWorkingBench;
        isWorking = false;
    }
    /*public void SetRecipe()
    {
        
        if(nameRicetta!="")
        {
            //ricetta1.nome = nameRicetta;
            ricetta1 = new RecipesManagerf.Ricetta(nameRicetta, ingredients);
            
            recipManager.IsSameReepi(ricetta1);

            workbenchPanel.CleanTable();
        }
        else //devo creare oggetti
        {
            ricetta1=new RecipesManagerf.Ricetta("create", ingredients);
            recipManager.IsSameReepi(ricetta1);
            workbenchPanel.CleanTable();
        }
        
    }*/
    public void SetRecipe()
    {
        if (nameRicetta != "")
        {
            ricetta1 = new RecipesManagerf.Ricetta(nameRicetta, ingredients);
        }
        else
        {
            ricetta1 = new RecipesManagerf.Ricetta("create", ingredients);
        }

        StartCoroutine(ruleClient.EvaluateWorkbenchRecipe(ricetta1, OnRecipeEvaluated));
    }
    private void OnRecipeEvaluated(RuleClient.RuleResponse response)
    {
        if (response == null)
        {
            Debug.LogError("Nessuna risposta dal backend");
            return;
        }

        Debug.Log(response.message);

        if (!response.allowed)
        {
            Debug.Log("Ricetta non valida");
            workbenchPanel.CleanTable();
            return;
        }

        recipManager.ApplyRecipeResult(
            ricetta1,
            response.action,
            response.resultItem,
            response.resultCount
        );
        workbenchPanel.CleanTable();
    }
    public void SetArray(string name,int count)
    {
        bool find = false;
        for(int i=0;i<ingredients.Count;i++)
        {
            if (ingredients[i].nome ==name)
            {
                ingredients[i].quantita++;
                find= true;
            }
        }
        if (!find)
        {
            ingredients.Add(new RecipesManagerf.Ingrediente(name, count));
        }
        
    }
}
