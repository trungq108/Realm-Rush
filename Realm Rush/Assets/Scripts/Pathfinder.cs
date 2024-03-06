using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinate;
    [SerializeField] Vector2Int destinationCoordinate;
    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up , Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();


    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null )
        {
            grid = gridManager.Grid;
        }
    }

    private void Start()
    {
        startNode = grid[startCoordinate];
        destinationNode = grid[destinationCoordinate];
        
        BreadthFirstSearch();
        BuildPath();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinate + direction;
            if(grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);        
            }
        }

        foreach(Node neighbor in neighbors)
        {
            if(!reached.ContainsKey(neighbor.coordinate) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinate, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch()
    {
        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoordinate, startNode);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if(currentSearchNode.coordinate == destinationCoordinate)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;
        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode.connectedTo);
            currentNode.isPath = true;
        }
        path.Reverse();

        return path;
    }
}
