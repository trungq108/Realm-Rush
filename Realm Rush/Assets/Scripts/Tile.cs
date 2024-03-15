using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceAble;
    public bool IsplayAble { get {  return isPlaceAble; } }

    Pathfinder pathfinder;
    GridManager gridManager;
    Vector2Int coordinate = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void Start()
    {
        if(gridManager != null)
        {
            coordinate = gridManager.GetCoordinatesFromPosition(transform.position);
            if(!isPlaceAble)
            {
                gridManager.BlockNode(coordinate);
            }
        }
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinate).isWalkable && !pathfinder.WillBlockPath(coordinate))
        {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinate);
                pathfinder.NotifyReceiver();
            }
        }     
    }
}
