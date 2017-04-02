using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System;

public class RandomMapTester : MonoBehaviour {

    [Header("Map Dimensions")]
    public int mapWidth = 20;
    public int mapHeight = 20;

    [Space]
    [Header("Vizualize Map")]
    public GameObject mapContainer;
    public GameObject tilePrefab;
    public Vector2 tileSize = new Vector2(24, 24);

    [Space]
    [Header("Map Sprites")]
    public Texture2D islandTexture;

    [Space]
    [Header("Decorate Map")]
    [Range(0, .9f)]
    public float erodePercent = .5f;
    public int erodeIterations = 2;
    [Range(0, .9f)]
    public float treePercent = .3f;
    [Range(0, .9f)]
    public float hillPercent = .2f;
    [Range(0, .9f)]
    public float mountaingPercent = .1f;
    [Range(0, .9f)]
    public float townPercent = .05f;
    [Range(0, .9f)]
    public float monsterPercent = .1f;
    [Range(0, .9f)]
    public float lakePercent = .05f;
    [Range(0, .9f)]
    public float castlePercent = .05f;


    public Map map;

	// Use this for initialization
	void Start () {
        map = new Map();
	}
	
	public void MakeMap()
    {
        map.NewMap(mapWidth,mapHeight);
        map.CreateIsland(
            erodePercent,
            erodeIterations,
            treePercent,
            hillPercent,
            mountaingPercent,
            townPercent,
            castlePercent,
            monsterPercent,
            lakePercent
            );
        string str = "";
        foreach (Tile t in map.tiles)
        {
            str += "Tile id: " + t.id + "\n";
            str += "Autotile Id: " + t.autotileID + "\n";
            str += "Neighbors:\n";
            for (int i = 0; i < t.neighbors.Length; i++)
            {
                if (t.neighbors[i] != null)
                    str += i + ":" + t.neighbors[i].id + "\n";
                else
                    str += i + ": Null \n";

            }
            str += "Edge Neighbors:\n";
            for (int i = 0; i < t.edgeNeighbors.Length; i++)
            {

                if (t.edgeNeighbors[i] != null)
                    str += i + ":" + t.edgeNeighbors[i].id + "\n";
                else
                    str += i + ": Null \n";

            }
        }
        Debug.Log(str);
        CreateGrid();
        
    }
    void CreateGrid()
    {
        ClearMapContainer();
        Sprite[] sprites = Resources.LoadAll<Sprite>(islandTexture.name);

        var total = map.tiles.Length;
        var maxColumns = map.columns;
        var column = 0;
        var row = 0;

        for(var i = 0; i < total; i++)
        {
            column = i % maxColumns;

            var newX = column * tileSize.x;
            var newY = -row * tileSize.y;

            var go = Instantiate(tilePrefab);
            go.name = "Tile " + i;
            go.transform.SetParent(mapContainer.transform);
            go.transform.position = new Vector3(newX, newY, 0);

            var tile = map.tiles[i];
            var spriteID = tile.autotileID;



            SpriteRenderer[] sr = go.GetComponentsInChildren<SpriteRenderer>();
            Debug.Log(sr.Length);
            
            if (spriteID >= 0)
            {
                spriteAssigner(sr, spriteID, sprites, tile);
            }
            if(column == (maxColumns -1))
            {
                row++;
            }
        }

    }
    void ClearMapContainer()
    {
        var children = mapContainer.transform.GetComponentsInChildren <Transform>();
        for(var i = children.Length - 1; i > 0; i--)
        {
            Destroy(children[i].gameObject);
        }
            
        
    }
    void spriteAssigner(SpriteRenderer[] sr, int spriteID, Sprite[] sprites, Tile tile)
    {
        TilesOrderDictionary order = new TilesOrderDictionary();
        for( int i =0; i< sr.Length;i++)
        {
            if(order.order.ContainsKey(spriteID))
                sr[i].sprite = sprites[order.order[spriteID][i]];
        }

        if(tile.edgeNeighbors[(int)EdgeSides.BottomLeft] == null)
        {
            if (tile.neighbors[(int)Sides.Bottom] != null
                && tile.neighbors[(int)Sides.Left] != null)
                if (tile.neighbors[(int)Sides.Bottom].autotileID >= 0
                    && tile.neighbors[(int)Sides.Left].autotileID >= 0)
                {
                    sr[2].sprite = sprites[order.findId(2, 1)];
                }
        }
        if (tile.edgeNeighbors[(int)EdgeSides.BottomRight] == null)
        {
            if (tile.neighbors[(int)Sides.Bottom] != null
                && tile.neighbors[(int)Sides.Right] != null)
                if (tile.neighbors[(int)Sides.Bottom].autotileID >= 0
                    && tile.neighbors[(int)Sides.Right].autotileID >= 0)
                {
                    sr[3].sprite = sprites[order.findId(3, 1)];
                }
        }
        if (tile.edgeNeighbors[(int)EdgeSides.TopLeft] == null)
        {
            if (tile.neighbors[(int)Sides.Top] != null
                && tile.neighbors[(int)Sides.Left] != null)
                if(tile.neighbors[(int)Sides.Top].autotileID >= 0 
                    && tile.neighbors[(int)Sides.Left].autotileID >= 0)
                {
                    sr[0].sprite = sprites[order.findId(2, 0)];
                }
        }
        if (tile.edgeNeighbors[(int)EdgeSides.TopRight] == null )
        {
            if (tile.neighbors[(int)Sides.Top] != null
                && tile.neighbors[(int)Sides.Right] != null)
                if (tile.neighbors[(int)Sides.Top].autotileID >= 0
                    && tile.neighbors[(int)Sides.Right].autotileID >= 0)
                {
                    sr[1].sprite = sprites[order.findId(3,0)];
                }
        }
    }
}
