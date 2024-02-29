using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerCost = 75;
    
    public bool CreateTower(Tower tower, Vector3 parent)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank.CurrentGold >= towerCost)
        {
            Instantiate(tower.gameObject, parent, Quaternion.identity);
            bank.DecreaseGold(towerCost);
            return true;
        }
        else { return false; }
    }
}
