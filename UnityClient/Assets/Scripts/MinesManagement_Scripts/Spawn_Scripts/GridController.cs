using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    //reference to the room?

    [System.Serializable]
    private struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }

    [SerializeField] private Grid grid;
    [SerializeField] private GameObject gridTile;
    [SerializeField] public List<Vector2> availablePoints = new List<Vector2>();

    private void Awake()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        for(int i=0; i<grid.rows; i++)
        {
            for(int j=0; j<grid.columns; j++)
            {
                GameObject tile = Instantiate(gridTile, transform);
                tile.transform.position = new Vector2(j-(grid.columns - grid.horizontalOffset), i-(grid.rows - grid.verticalOffset));
                availablePoints.Add(tile.transform.position);
            }
        }

        FindAnyObjectByType<Spawner>().InitializeSpawn();
    }
}
