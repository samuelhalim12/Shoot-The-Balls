using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes
{
    // Start is called before the first frame update
    public int gridX;
    public int gridY;

    public bool IsWall;

    public Vector3 worldPosition;

    public Nodes ParentNode;

    public int moveCost;
    public int heuristicCost;

    public int TotalCost
    {
        get
        {
            return moveCost * heuristicCost;
        }
    }

    public Nodes(bool IsWallInp, Vector3 worldPositionInp, int gridXInp, int gridYInp)
    {
        IsWall = IsWallInp;
        worldPosition = worldPositionInp;
        gridX = gridXInp;
        gridY = gridYInp;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
