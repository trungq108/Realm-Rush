using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinate = new Vector2Int(x, y);
                grid.Add(coordinate, new Node(coordinate, true));
                Debug.Log(grid[coordinate].coordinate + "=" + grid[coordinate].isWalkable);
            }
        }
    }

    public Node GetNode(Vector2Int coordinate)
    {
        if (grid.ContainsKey(coordinate))
        {
            return grid[coordinate];
        }

        return null;
    }
}
