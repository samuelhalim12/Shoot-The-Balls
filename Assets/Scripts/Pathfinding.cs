using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    GridBlock gridRef;
    public Transform startPos;
    public Transform targetPos;

    private void Awake()
    {
        gridRef = GetComponent<GridBlock>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FindPath(startPos.position, targetPos.position);
    }
    void FindPath(Vector3 startPosInp, Vector3 targetPosInp)
    {
        Nodes startNode = gridRef.NodeFromWorldPoint(startPosInp);
        Nodes targetNode = gridRef.NodeFromWorldPoint(targetPosInp);

        List<Nodes> openList = new List<Nodes>();
        HashSet<Nodes> closedList = new HashSet<Nodes>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Nodes currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].TotalCost < currentNode.TotalCost || openList[i].TotalCost == currentNode.TotalCost)
                {
                    if (openList[i].heuristicCost < currentNode.heuristicCost)
                    {
                        currentNode = openList[i];
                    }
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            if (currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
                return;
            }
            foreach (Nodes neighborNode in gridRef.GetNeighboringNodes(currentNode))
            {
                if (neighborNode.IsWall || closedList.Contains(neighborNode))
                {
                    continue;
                }
                int costToNeighbor = currentNode.moveCost + GetManhattanDistance(currentNode, neighborNode);

                if (costToNeighbor < neighborNode.moveCost || !openList.Contains(neighborNode))
                {
                    neighborNode.moveCost = costToNeighbor;
                    neighborNode.heuristicCost = GetManhattanDistance(neighborNode, targetNode);
                    neighborNode.ParentNode = currentNode;
                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }
    }
    void GetFinalPath(Nodes startNodeInp, Nodes endNoeInp)
    {
        List<Nodes> finalPath = new List<Nodes>();
        Nodes currentNode = endNoeInp;
        while (currentNode != startNodeInp)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.ParentNode;

        }
        finalPath.Reverse();
        gridRef.finalPath = finalPath;
    }
    int GetManhattanDistance(Nodes nodeA, Nodes nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return distanceX + distanceY;
    }
}
