using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyGold = 25;
    [SerializeField] int stealGold = 25;

    Bank bank;

    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void KillReward()
    {
        bank.AddGold(enemyGold);
    }

    public void StealGold()
    {
        bank.DecreaseGold(stealGold);
    }

}
