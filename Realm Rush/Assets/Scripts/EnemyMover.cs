using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new List<WayPoint>();

    void Start()
    {
        PrintWaypointsName();
    }

    private void PrintWaypointsName()
    {
        foreach (WayPoint wayPoint in path)
        {
            Debug.Log(wayPoint);
        }
    }

}
