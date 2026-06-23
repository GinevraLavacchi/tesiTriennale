using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private bool loadAfterSceneChange = false;
    private GameData gameData;
    private List<DataPersistanceInterface> dataPersistanceObjects;
    private FileDataHandler dataHandler;
    private string selectedProfile = "test";
    //singleton
    public static DataPersistanceManager instance { get; private set; }

    private void Awake()
    {
        //Application.persistent data path will give the operating system standard directory for persisting data in a unity project
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this.gameObject);

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();

        if (loadAfterSceneChange)
        {
            LoadGame();
            loadAfterSceneChange = false;
        }
    }
    public void LoadGameAfterSceneChange()
    {
        loadAfterSceneChange = true;
    }
    public void OnSceneUnloaded(Scene scene)
    {
        // NON salvare al cambio scena
    }

    public void ChangeSelectedProfile(string newProfile)
    {
        this.selectedProfile = newProfile;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {

        gameData = dataHandler.Load(selectedProfile);

        if (gameData == null)
        {
            return;
        }

        dataPersistanceObjects = FindAllDataPersistanceObjects();

        foreach (DataPersistanceInterface obj in dataPersistanceObjects)
        {
        }

        foreach (DataPersistanceInterface dataPersistance in dataPersistanceObjects)
        {
            dataPersistance.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        if (this.gameData == null)
        {
            NewGame();
        }

        this.dataPersistanceObjects = FindAllDataPersistanceObjects();

        foreach (DataPersistanceInterface dataPersistance in dataPersistanceObjects)
        {
            dataPersistance.SaveData(ref gameData);
        }

        dataHandler.Save(gameData, selectedProfile);

    }
    private void OnApplicationQuit()
    {
       // SaveGame(); //the game gets saved everytime the application is closed
    }
    private List<DataPersistanceInterface> FindAllDataPersistanceObjects()
    {
        IEnumerable<DataPersistanceInterface> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<DataPersistanceInterface>();

        return new List<DataPersistanceInterface>(dataPersistanceObjects);
    }

    public bool HasGameData()
    {
        return this.gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }

    public GameData GetGameData()
    {
        return gameData;
    }
    public void SaveGameButton()
    {
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        SaveGame();
    }

}
