using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }
    [SerializeField] int unityGridSize = 5; // = Unity snap setting
    public int UnityGridSize { get { return unityGridSize; } }

    private void Awake()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
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

    public void BlockNode(Vector2Int coordinate)
    {
        if (grid.ContainsKey(coordinate))
        {
            grid[coordinate].isWalkable = false;
        }
    }

    public Vector2Int GetCoordinatesFormPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFormCoordinates(Vector2Int coordinate)
    {
        Vector3 position = new Vector3();
        position.x = Mathf.RoundToInt(coordinate.x * unityGridSize);
        position.y = 0;
        position.z = Mathf.RoundToInt(coordinate.y * unityGridSize);

        return position;
    }
}
