using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public event Action OnUpgradeHealthClick;
    public event Action OnUpgradeSpeedClick;
    public event Action OnUpgradeDamageClick;
    
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
        SetUp(0, settings.Health, settings.Damage, settings.Speed, 1, 1, 1, 
            settings.FirstUpgradePrice, false, settings.Health);
    }

    public void SetUp(int coins, int health, int damage, int speed, int speedLevel, int damageLevel, int healthLevel,
        int upgradePrice, bool upgradeButtonsInteractivity, int? currentHealth = null)
    {
        upgradeMenuController.SetUp(coins, health, damage, speed, healthLevel, damageLevel, speedLevel,
            upgradePrice, upgradeButtonsInteractivity);
        topPanelController.SetUp(coins, health, damage, speed, currentHealth);
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

    public void EnableUpgrade()
    {
        upgradeMenuController.EnableUpgradeButtons(true);
    }

    public void ShowDamageBooster(int damageAmount, int timer)
    {
        topPanelController.ShowDamageBooster(damageAmount, timer);
    }

    public void ShowSpeedBooster(int speedAmount, int timer)
    {
        topPanelController.ShowSpeedBooster(speedAmount, timer);
    }

    private void Awake()
    {
        upgradeMenuController.OnUpgradeDamageClick += OnUpgradeDamage;
        upgradeMenuController.OnUpgradeHealthClick += OnUpgradeHealth;
        upgradeMenuController.OnUpgradeSpeedClick += OnUpgradeSpeed;
    }

    private void OnUpgradeHealth()
    {
        OnUpgradeHealthClick?.Invoke();
    }

    private void OnUpgradeSpeed()
    {
        OnUpgradeSpeedClick?.Invoke();
    }

    private void OnUpgradeDamage()
    {
        OnUpgradeDamageClick?.Invoke();
    }

    private void OnDestroy()
    {
        upgradeMenuController.OnUpgradeDamageClick -= OnUpgradeDamage;
        upgradeMenuController.OnUpgradeHealthClick -= OnUpgradeHealth;
        upgradeMenuController.OnUpgradeSpeedClick -= OnUpgradeSpeed;
    }
}
