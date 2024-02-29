using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceAble;
    public bool IsplayAble { get {  return isPlaceAble; } } 

    private void OnMouseDown()
    {
        if (isPlaceAble)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceAble = !isPlaced;
        }     
    }
}
