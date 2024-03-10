using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0,5)] float moveSpeed;

    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathFinder;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<Pathfinder>();
    }

    private void OnEnable()
    {
        FindPath();
        ReturnToStartPos();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        path.Clear();
        path = pathFinder.BuildPath();
    }

    void ReturnToStartPos()
    {
        transform.position = gridManager.GetPositionFormCoordinates(pathFinder.StartCoordinate);
    }

    IEnumerator FollowPath()
    {
        for(int i = 0; i < path.Count; i++) 
        {
            Vector3 startPosition = transform.position;          
            Vector3 endPosition = gridManager.GetPositionFormCoordinates(path[i].coordinate);
            float travelPercent = 0f;
            transform.LookAt(endPosition);

            while (travelPercent < 1f) 
            {
                Debug.Log("ok ne");
                travelPercent += Time.deltaTime * moveSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }       
        }

        FinishPath();
    }

    void FinishPath()
    {
        enemy.StealGold();
        this.gameObject.SetActive(false);
    }
}
