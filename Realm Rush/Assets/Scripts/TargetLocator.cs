using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectTile;
    [SerializeField] float towerRange = 15f;
    Transform target;

    void Update()
    {
        FindClosettEnemy();
        WeaponAim();
    }

    void WeaponAim()
    {
        weapon.LookAt(target);

        float targetDistance = Vector3.Distance(transform.position, target.position);
        if (targetDistance < towerRange) { Attack(true); }
        else { Attack(false); }
    }

    void FindClosettEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closetEnemy = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position,enemy.transform.position);
            if(targetDistance < maxDistance)
            {
                closetEnemy = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closetEnemy;
    }

    void Attack(bool isActive)
    {
        var emission = projectTile.emission;
        emission.enabled = isActive;
    }
}
