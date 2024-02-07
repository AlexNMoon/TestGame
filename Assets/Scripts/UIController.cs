using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] 
    private GameObject background;
    [SerializeField] 
    private GameObject gameOverPanel;
    [SerializeField] 
    private GameObject upgradeMenuPanel;
    [SerializeField] 
    private TopPanelController topPanelController;

    public void SetUp(int coinsAmount, int currentHealthAmount, int healthAmount, int strengthAmount, int dexterityAmount)
    {
        topPanelController.SetUp(coinsAmount, currentHealthAmount, healthAmount, strengthAmount, dexterityAmount);
    }

    public void ShowGameOver()
    {
        background.SetActive(true);
        gameOverPanel.SetActive(true);
    }

    public void ShowUpgradeMenu(bool show)
    {
        background.SetActive(show);
        upgradeMenuPanel.SetActive(show);
    }

    public void ChangeCoins(int coins)
    {
        topPanelController.SetCoins(coins);
    }

    public void ChangeCurrentHealth(int health)
    {
        topPanelController.ChangeCurrentHealth(health);
    }
}
