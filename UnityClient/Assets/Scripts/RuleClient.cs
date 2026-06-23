using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RuleClient : MonoBehaviour
{
    [Serializable]
    public class RuleRequest
    {
        public string targetType;
        public string recipeName;
        public List<InventoryItemDto> ingredients;
    }

    [Serializable]
    public class InventoryItemDto
    {
        public string type;
        public int count;

        public InventoryItemDto(string type, int count)
        {
            this.type = type;
            this.count = count;
        }
    }

    [Serializable]
    public class RuleResponse
    {
        public bool allowed;
        public string action;
        public string message;
        public string resultItem;
        public int resultCount;
    }
    [SerializeField] bool useQuarkus;

    private string url = "http://localhost:8080/rules/evaluate";
    private void Update()
    {
        if (useQuarkus)
        {
            url = "http://localhost:8081/rules/evaluate";
        }
        else { url = "http://localhost:8080/rules/evaluate"; }
    }
    public IEnumerator EvaluateWorkbenchRecipe(
        RecipesManagerf.Ricetta ricetta,
        Action<RuleResponse> onResult
    )
    {
        RuleRequest request = new RuleRequest();
        request.targetType = "workbench";
        request.recipeName = ricetta.nome;
        request.ingredients = new List<InventoryItemDto>();

        foreach (var ingrediente in ricetta.ingredienti)
        {
            request.ingredients.Add(
                new InventoryItemDto(ingrediente.nome, ingrediente.quantita)
            );
        }

        string json = JsonUtility.ToJson(request);
        Debug.Log("JSON inviato al backend: " + json);
        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Errore backend: " + webRequest.error);
            onResult(null);
            yield break;
        }

        RuleResponse response =
            JsonUtility.FromJson<RuleResponse>(webRequest.downloadHandler.text);
        Debug.Log("Risposta backend: " + webRequest.downloadHandler.text);
        onResult(response);
    }
}