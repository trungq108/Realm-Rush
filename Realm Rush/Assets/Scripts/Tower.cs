using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerCost = 75;
    [SerializeField] float buildDelay = 1f;

    private void Start()
    {
        StartCoroutine(Build());
    }

    IEnumerator Build()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform child2 in child.transform)
            {
                child2.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform child2 in child.transform)
            {
                child2.gameObject.SetActive(true);
            }
        }
    }

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
