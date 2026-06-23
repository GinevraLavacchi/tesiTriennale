using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class FieldManager : MonoBehaviour
{
    public Tilemap tilemap;
    [SerializeField] private Tile wateredTile; // Tile dopo essere stata annaffiata
    [SerializeField] private Tile normalTile;  // Tile "normale"
    [SerializeField] private Tile borderWateredLeft;
    [SerializeField] private Tile borderWateredRight;
    [SerializeField] private Tile borderWateredUp;
    [SerializeField] private Tile borderWateredDown;
    [SerializeField] private Tile borderAngleTopLeft;
    [SerializeField] private Tile borderAngleTopRight;
    [SerializeField] private Tile borderAngleDownLeft;
    [SerializeField] private Tile borderAngleDownRight;
    [SerializeField] private Tile borderNormalLeft;
    [SerializeField] private Tile borderNormalRight;
    [SerializeField] private Tile borderNormalUp;
    [SerializeField] private Tile borderNormalDown;
    [SerializeField] private Tile borderAngleNormalTopLeft;
    [SerializeField] private Tile borderAngleNormalTopRight;
    [SerializeField] private Tile borderAngleNormalDownLeft;
    [SerializeField] private Tile borderAngleNormalDownRight;

    public Tilemap waterTile;
    [SerializeField] private TileBase workedTile;
    [SerializeField] private TileBase wateredWorkedTile;

    public LayerMask wheatLayer;
    public LayerMask tomatoLayer;
    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void Update()
    {
        if(SceneManager.GetActiveScene().name == "SampleScene")
        {
            GameObject waterTileGameObject = GameObject.FindWithTag("Water");
            waterTile = waterTileGameObject.GetComponent<Tilemap>();
            GameObject fieldTileGameObject = GameObject.FindWithTag("Field");
            tilemap = fieldTileGameObject.GetComponent<Tilemap>();

        }
    }
    public bool IsWaterable(Vector3Int position)
    {
        if (tilemap == null) return false;
        TileBase tile = tilemap.GetTile(position);
        if(tile!=null)
        {
            if (tile.name.StartsWith("Tilled_Dirt_v2_11") || tile.name.StartsWith("FieldTile_61")) 
            {
                return true;
            }
        }
        return false;
    }
    public void IsBorder(Vector3Int position)
    {
        if (tilemap == null) return;
        TileBase tile = tilemap.GetTile(position);
        //Debug.Log(tile.name);
        if (tile != null)
        {
            if (tile.name.StartsWith("WateredGound"))
            {
                Vector3Int left = position;
                left.x = left.x - 1;
                TileBase tileLeft = tilemap.GetTile(left);
                if (tileLeft.name == "Tilled_Dirt_v2_59")
                {
                    SetBorder(left, "left", true);
                }
                Vector3Int right = position;
                right.x = right.x + 1;
                TileBase tileRight = tilemap.GetTile(right);
                if (tileRight.name == "Tilled_Dirt_v2_60")
                {
                    SetBorder(right, "right", true);
                }
                Vector3Int up = position;
                up.y = up.y + 1;
                TileBase tileUp = tilemap.GetTile(up);
                if (tileUp.name == "Tilled_Dirt_v2_56")
                {
                    SetBorder(up, "up", true);
                }
                Vector3Int down = position;
                down.y = down.y - 1;
                TileBase tileDown = tilemap.GetTile(down);
                if (tileDown.name == "Tilled_Dirt_v2_66")
                {
                    SetBorder(down, "down", true);
                }

                //angolo sinistratop ha x-1 e y+1
                //angolo destratop ha x+1 e y+1
                //angolo sinistradown ha x-1 e y-1
                //angolo destradown ha x+1 e y-1
                Vector3Int Topleft = position;
                Topleft.x = Topleft.x - 1;
                Topleft.y = Topleft.y + 1;
                TileBase tileTopLeft = tilemap.GetTile(Topleft);
                if (tileTopLeft.name == "Tilled_Dirt_v2_0")
                {
                    SetBorder(Topleft, "topLeft", true);
                }
                Vector3Int TopRight = position;
                TopRight.x = TopRight.x + 1;
                TopRight.y = TopRight.y + 1;
                TileBase tileTopRight = tilemap.GetTile(TopRight);
                if (tileTopRight.name == "Tilled_Dirt_v2_2")
                {
                    SetBorder(TopRight, "topRight", true);
                }
                Vector3Int DownRight = position;
                DownRight.x = DownRight.x + 1;
                DownRight.y = DownRight.y - 1;
                TileBase tileDownRight = tilemap.GetTile(DownRight);
                if (tileDownRight.name == "Tilled_Dirt_v2_22")
                {
                    SetBorder(DownRight, "downRight", true);
                }
                Vector3Int DownLeft = position;
                DownLeft.x = DownLeft.x - 1;
                DownLeft.y = DownLeft.y - 1;
                TileBase tileDownLeft = tilemap.GetTile(DownLeft);
                if (tileDownLeft.name == "Tilled_Dirt_v2_20")
                {
                    SetBorder(DownLeft, "downLeft", true);
                }
            }
            else if (tile.name.StartsWith("Tilled_Dirt_v2_11"))
            {
                Vector3Int left = position;
                left.x = left.x - 1;
                TileBase tileLeft = tilemap.GetTile(left);
                if (tileLeft.name == "WateredGound_59")
                {
                    SetBorder(left, "left", false);
                }
                Vector3Int right = position;
                right.x = right.x + 1;
                TileBase tileRight = tilemap.GetTile(right);
                if (tileRight.name == "WateredGound_60")
                {
                    SetBorder(right, "right", false);
                }
                Vector3Int up = position;
                up.y = up.y + 1;
                TileBase tileUp = tilemap.GetTile(up);
                if (tileUp.name == "WateredGound_58")
                {
                    SetBorder(up, "up", false);
                }
                Vector3Int down = position;
                down.y = down.y - 1;
                TileBase tileDown = tilemap.GetTile(down);
                if (tileDown.name == "WateredGound_67")
                {
                    SetBorder(down, "down", false);
                }

                //angolo sinistratop ha x-1 e y+1
                //angolo destratop ha x+1 e y+1
                //angolo sinistradown ha x-1 e y-1
                //angolo destradown ha x+1 e y-1
                Vector3Int Topleft = position;
                Topleft.x = Topleft.x - 1;
                Topleft.y = Topleft.y + 1;
                TileBase tileTopLeft = tilemap.GetTile(Topleft);
                if (tileTopLeft.name == "WateredGound_0")
                {
                    SetBorder(Topleft, "topLeft", false);
                }
                Vector3Int TopRight = position;
                TopRight.x = TopRight.x + 1;
                TopRight.y = TopRight.y + 1;
                TileBase tileTopRight = tilemap.GetTile(TopRight);
                if (tileTopRight.name == "WateredGound_2")
                {
                    SetBorder(TopRight, "topRight", false);
                }
                Vector3Int DownRight = position;
                DownRight.x = DownRight.x + 1;
                DownRight.y = DownRight.y - 1;
                TileBase tileDownRight = tilemap.GetTile(DownRight);
                if (tileDownRight.name == "WateredGound_22")
                {
                    SetBorder(DownRight, "downRight", false);
                }
                Vector3Int DownLeft = position;
                DownLeft.x = DownLeft.x - 1;
                DownLeft.y = DownLeft.y - 1;
                TileBase tileDownLeft = tilemap.GetTile(DownLeft);
                if (tileDownLeft.name == "WateredGound_20")
                {
                    SetBorder(DownLeft, "downLeft", false);
                }
            }
        }
    }
    public void SetWatered(Vector3Int position)
    {
        if (tilemap == null) return;
        TileBase tile = tilemap.GetTile(position);
        //Debug.Log(tile.name);
        if (tile != null)
        {
            if (tile.name.StartsWith("Tilled_Dirt_v2_11")||tile.name.StartsWith("FieldTile_11"))
            {
                tilemap.SetTile(position, wateredTile);
            }
            else if (tile.name.StartsWith("FieldTile_61"))
            {
                tilemap.SetTile(position, wateredWorkedTile);
            }
        }
    }

    public void SetBorder(Vector3Int position,string direction,bool towater)
    {
        if (tilemap == null) return;
        if ((towater))
        {
            if (direction == "left")
            {
                tilemap.SetTile(position, borderWateredLeft);
            }
            if (direction == "right")
            {
                tilemap.SetTile(position, borderWateredRight);
            }
            if (direction == "up")
            {
                tilemap.SetTile(position, borderWateredUp);
            }
            if (direction == "down")
            {
                tilemap.SetTile(position, borderWateredDown);
            }
            if (direction == "topLeft")
            {
                tilemap.SetTile(position, borderAngleTopLeft);
            }
            if (direction == "topRight")
            {
                tilemap.SetTile(position, borderAngleTopRight);
            }
            if (direction == "downLeft")
            {
                tilemap.SetTile(position, borderAngleDownLeft);
            }
            if (direction == "downRight")
            {
                tilemap.SetTile(position, borderAngleDownRight);
            }
        }
        else
        {
            if (direction == "left")
            {
                tilemap.SetTile(position, borderNormalLeft);
            }
            if (direction == "right")
            {
                tilemap.SetTile(position, borderNormalRight);
            }
            if (direction == "up")
            {
                tilemap.SetTile(position, borderNormalUp);
            }
            if (direction == "down")
            {
                tilemap.SetTile(position, borderNormalDown);
            }
            if (direction == "topLeft")
            {
                tilemap.SetTile(position, borderAngleNormalTopLeft);
            }
            if (direction == "topRight")
            {
                tilemap.SetTile(position, borderAngleNormalTopRight);
            }
            if (direction == "downLeft")
            {
                tilemap.SetTile(position, borderAngleNormalDownLeft);
            }
            if (direction == "downRight")
            {
                tilemap.SetTile(position, borderAngleNormalDownRight);
            }
        }
        
    }

    public bool RefillWater(Vector3Int position)
    {
        if (tilemap == null) return false;
        //Debug.Log(position.ToString());
        TileBase tile= waterTile.GetTile(position);
        if(tile!=null)
        {
            if(tile.name=="WaterAnimation")
            {
                return true;
            }
        }
        return false;
    }

    public bool IsZappable(Vector3Int position)
    {
        if (tilemap == null) return false;
        TileBase tile = tilemap.GetTile(position);
        if (tile != null)
        {
            if (tile.name.StartsWith("Tilled_Dirt_v2_11")|| tile.name.StartsWith("WateredGound_11")||tile.name.StartsWith("FieldTile_11"))
            {
                return true;
            }
        }
        return false;
    }

    public void SetWorked(Vector3Int position)
    {
        if (tilemap == null) return;
        TileBase tile = tilemap.GetTile(position);
        if (tile != null)
        {
            if (tile.name.StartsWith("Tilled_Dirt_v2_11")||tile.name.StartsWith("FieldTile_11"))
            {
                tilemap.SetTile(position, workedTile);
            }
            else if(tile.name.StartsWith("Watered"))
            {
                tilemap.SetTile(position, wateredWorkedTile);
            }
        }
    }
    public bool IsPlantable(Vector3Int position)
    {
        if (tilemap == null) return false; 
        TileBase tile = tilemap.GetTile(position);
        if (tile != null)
        {
//Debug.Log(tile.name);
            if (tile.name.StartsWith("WateredGound_61"))
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Collider2D[] items1 = Physics2D.OverlapCircleAll(player.transform.position, 0.2f, tomatoLayer);
                Collider2D[] items = Physics2D.OverlapCircleAll(player.transform.position, 0.2f, wheatLayer);
                
                if (items.Length <= 0&&items1.Length<=0)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void SetNormal(Vector3Int position)
    {
        if (tilemap == null) return;
        TileBase tile = tilemap.GetTile(position);
        if (tile != null)
        {
            tilemap.SetTile(position, normalTile);
        }
    }
}
