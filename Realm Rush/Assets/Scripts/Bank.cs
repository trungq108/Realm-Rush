using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startGold = 150;
    [SerializeField] int currentGold;
    [SerializeField] TextMeshProUGUI goldText;
    

    private void Start()
    {
        currentGold = startGold;
        UpdateDisPlay();
    }

    public int CurrentGold { get { return currentGold; } }

    public void AddGold(int amount)
    {
        currentGold += Mathf.Abs(amount);
        UpdateDisPlay();
    }

    public void DecreaseGold(int amount)
    {
        currentGold -= Mathf.Abs(amount);
        UpdateDisPlay();

        if (currentGold < 0) { ReloadScene(); }
    }

    void UpdateDisPlay()
    {
        goldText.text = "Gold: " + currentGold.ToString();
    }

    void ReloadScene()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex);
    }
}
