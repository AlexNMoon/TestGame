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
    [SerializeField] 
    private UpgradeMenuController upgradeMenuController;

    public void SetUp(PlayerSettingsSO settings)
    {
        upgradeMenuController.SetUp(settings);
        topPanelController.SetUp(0, settings.Health, settings.Health, settings.Speed, settings.Damage);
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
        upgradeMenuController.ChangeCoins(coins);
    }

    public void ChangeCurrentHealth(int health)
    {
        topPanelController.ChangeCurrentHealth(health);
    }
}
