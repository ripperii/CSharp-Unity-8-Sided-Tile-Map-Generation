using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Xml.Serialization;
[Serializable]
public enum Sides
{
    Bottom,
    Right,
    Left,
    Top
}
public enum EdgeSides
{
    BottomRight,
    BottomLeft,
    TopRight,
    TopLeft
}
public class Tile {

    public int id = 0;
    public Tile[] neighbors = new Tile[4];
    public Tile[] edgeNeighbors = new Tile[4];
    public int autotileID;
    public void AddHeightbor(Sides side, Tile tile)
    {
        neighbors[(int)side] = tile;
        CalculateAutotileID();
    }
    public void AddEdgeNeighbor(EdgeSides side, Tile tile)
    {
        edgeNeighbors[(int)side] = tile;
    }
    public void RemoveNeighbor(Tile tile)
    {
        var total = neighbors.Length;
        for(var i = 0; i<total;i++)
        {
            if(neighbors[i] != null)
            {
                if(neighbors[i].id == tile.id)
                {
                    neighbors[i] = null;
                }
            }
        }
        CalculateAutotileID();
    }
    public void RemoveEdgeNeighbor(Tile tile)
    {
        var total = edgeNeighbors.Length;
        for (var i = 0; i < total; i++)
        {
            if (edgeNeighbors[i] != null)
            {
                if (edgeNeighbors[i].id == tile.id)
                {
                    edgeNeighbors[i] = null;
                }
            }
        }
    }
    public void ClearNeighbors()
    {
        var total = neighbors.Length;
        for (var i = 0; i < total; i++)
        {
            var tile = neighbors[i];
            if(tile != null)
            {
                tile.RemoveNeighbor(this);
                neighbors[i] = null;
            }
        }

        CalculateAutotileID();

        total = edgeNeighbors.Length;
        for (var i = 0; i < total; i++)
        {
            var tile = edgeNeighbors[i];
            if (tile != null)
            {
                tile.RemoveEdgeNeighbor(this);
                edgeNeighbors[i] = null;
            }
        }
    }
    private void CalculateAutotileID()
    {
        var sideValues = new StringBuilder();
        foreach(Tile tile in neighbors)
        {
            sideValues.Append(tile == null ? "0" : "1");
        }
        autotileID = Convert.ToInt32(sideValues.ToString(), 2);
    }

}
