using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinate;
    public Vector2Int StartCoordinate { get { return startCoordinate; } }

    [SerializeField] Vector2Int destinationCoordinate;
    public Vector2Int DestinationCoordinate { get { return destinationCoordinate; } }


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

        if (gridManager != null )
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinate];
            destinationNode = grid[destinationCoordinate];
            
        }
    }

    private void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
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
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

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

    public List<Node> BuildPath()
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

    public bool WillBlockPath(Vector2Int coordinate)
    {
        if(grid.ContainsKey(coordinate))
        {
            bool previousState = grid[coordinate].isWalkable;

            grid[coordinate].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinate].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }
}
