using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoint = 5            ;
    int currentHitPoint = 0;

    private void Start()
    {
        currentHitPoint = maxHitPoint;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoint--;
        if (currentHitPoint == 0)
        {
            Destroy(gameObject);
        }
    }
}
