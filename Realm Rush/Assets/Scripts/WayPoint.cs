using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    [SerializeField] bool isPlayAble;
    public bool IsplayAble { get {  return isPlayAble; } } 

    private void OnMouseDown()
    {
        if (!isPlayAble) { return; }
        Instantiate(towerPrefab,transform.position, Quaternion.identity);
        isPlayAble = true;
    }
}
