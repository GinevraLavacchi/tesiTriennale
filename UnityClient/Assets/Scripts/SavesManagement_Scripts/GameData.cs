using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    
    public int day;
    public int hour;
    public int minute;
    public float elapsedTime;

    //public Vector3 playerPosition;

    public List<Inventory.Slot> slots;
    public List<Inventory.Slot> chest;
    public List<PlayerHarvest.PlantPref> wheats;
    public List<PlayerHarvest.PlantPref> tomatos;
    //public string activeScene;

    /*public int hour;
    public int minute;
    condizione terreno
    piante
    inventario
    chest*/

    // the values in the constructor are the ones you start with when starting a new game
    public GameData()
    {
        day = 1;
        //playerPosition = new Vector3(17.5f, -3f, 0);
        hour = 6;
        minute = 0;
        elapsedTime = 6 * 60f / 100;
        wheats = new List<PlayerHarvest.PlantPref>();
        tomatos = new List<PlayerHarvest.PlantPref>();
        slots = new List<Inventory.Slot>();
        chest = new List<Inventory.Slot>();
        //activeScene = "";
    }

}
