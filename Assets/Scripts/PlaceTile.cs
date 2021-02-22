using DungeonGeneration;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlaceTile : MonoBehaviour
{
    [SerializeField]
    int m_index;
    public Image[] PlaceImages;
    [SerializeField]
    List<CustomTile> m_customTilesToPlace = new List<CustomTile>();
    public float MaxRange;
    public TextMeshProUGUI BlockInfo;

    public void FillTilesList()
    {
        for (int i = 0; i < TileManager.GetTileHolder(TileType.Wall).Tiles.Count; ++i)
        {
            if (!m_customTilesToPlace.Contains(TileManager.GetTileHolder(TileType.Wall).Tiles[i]))
                m_customTilesToPlace.Add(TileManager.GetTileHolder(TileType.Wall).Tiles[i]);
        }
        for (int i = 0; i < TileManager.GetTileHolder(TileType.Floor).Tiles.Count; ++i)
        {
            if (!m_customTilesToPlace.Contains(TileManager.GetTileHolder(TileType.Floor).Tiles[i]))
                m_customTilesToPlace.Add(TileManager.GetTileHolder(TileType.Floor).Tiles[i]);
        }
   
    }
    void SwitchBlockWithInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_index = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_index = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_index = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_index = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            m_index = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            m_index = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            m_index = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            m_index = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            m_index = 8;
        }
    }
    private void Start()
    {
        for (int i = 0; i < m_customTilesToPlace.Count; ++i)
        {
            PlaceImages[i].color = m_customTilesToPlace[i].TileColour;
        }
    }
    void Update()
    {
        SwitchBlockWithInput();
        for (int i = 0; i < PlaceImages.Length; ++i)
        {
            if(i == m_index)
            {
                PlaceImages[i].color = m_customTilesToPlace[i].TileColour;
            }
            else
            {
                PlaceImages[i].color = new Color(m_customTilesToPlace[i].TileColour.r, m_customTilesToPlace[i].TileColour.g, m_customTilesToPlace[i].TileColour.b, 0.3f);
            }
         
        }
        BlockInfo.text = m_customTilesToPlace[m_index].name + "\nType: " + m_customTilesToPlace[m_index].Type.ToString();
        float d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            m_index++;
            if (m_index > m_customTilesToPlace.Count - 1)
            {
                m_index = 0;
            }
        }
        else if (d < 0f)
        {
            m_index--;
            if (m_index < 0)
            {
                m_index = m_customTilesToPlace.Count - 1;
            }
        }
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int v = new Vector3Int((int)worldPosition.x, (int)worldPosition.y, 0);
        float distance = Vector3Int.Distance(v, new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z));
        if (distance <= MaxRange)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (WallGen.GetTilemap().GetTile(v) == null)
                {
                    if (m_customTilesToPlace[m_index].Type == TileType.Wall)
                    {
                        TileManager.PlaceTile(v, m_index, DungeonUtility.GetTilemap(), WallGen.GetTilemap(), m_customTilesToPlace);
                       // Debug.Log("Building Tile: " + WallGen.GetTilemap().GetTile(v).name + worldPosition);
                    }
                }
                if (DungeonUtility.GetTilemap().GetTile(v) != null  && WallGen.GetTilemap().GetTile(v) == null)
                {
                    if (m_customTilesToPlace[m_index].Type == TileType.Floor)
                    {
                        TileManager.PlaceTile(v, m_index ,DungeonUtility.GetTilemap(), DungeonUtility.GetTilemap(),m_customTilesToPlace);
                      //  Debug.Log("Building Tile: " + DungeonUtility.GetTilemap().GetTile(v).name + worldPosition);
                    }
                }
            }
        }
    }
}