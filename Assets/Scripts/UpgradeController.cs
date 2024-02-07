using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeController
{
    private int _speed;
    private int _speedBonus;
    private int _health;
    private int _currentHealth;
    private int _damage ;
    private int _damageBonus;
    private int _speedUpgradeIncrement;
    private int _healthUpgradeIncrement;
    private int _damageUpgradeIncrement;
    private int _upgradePrice;
    private int _upgradePriceIncrement;
    private int _currentCoins;
    private int _speedLevel = 1;
    private int _healthLevel = 1;
    private int _damageLevel = 1;

    private UIController _uiController;
    private PlayerController _playerController;

    public UpgradeController(PlayerSettingsSO settings)
    {
        _speed = settings.Speed;
        _health = settings.Health;
        _damage = settings.Damage;
        _speedUpgradeIncrement = settings.SpeedUpgradeIncrement;
        _healthUpgradeIncrement = settings.HealthUpgradeIncrement;
        _damageUpgradeIncrement = settings.DamageUpgradeIncrement;
        _upgradePrice = settings.FirstUpgradePrice;
        _upgradePriceIncrement = settings.UpgradePriceIncrement;
    }

    public void SetDependencies(UIController uiController, PlayerController player)
    {
        _uiController = uiController;
        _playerController = player;
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _uiController.OnUpgradeDamageClick += OnUpgradeDamage;
        _uiController.OnUpgradeHealthClick += OnUpgradeHealth;
        _uiController.OnUpgradeSpeedClick += OnUpgradeSpeed;
    }

    private void OnUpgradeDamage()
    {
        _damage += _damageUpgradeIncrement;
        _damageLevel++;
        _playerController.ChangeDamage(_damage);
        PayForUpgrade();
        UpdateUIController();
    }

    private void PayForUpgrade()
    {
        _currentCoins -= _upgradePrice;
        _upgradePrice += _upgradePriceIncrement;
    }

    private void UpdateUIController()
    {
        bool upgradesAvailable = _currentCoins >= _upgradePrice;
        _uiController.SetUp(_currentCoins, _health, _damage, _speed, _speedLevel, 
            _damageLevel, _healthLevel, _upgradePrice, upgradesAvailable);
    }

    private void OnUpgradeHealth()
    {
        _health += _healthUpgradeIncrement;
        _healthLevel++;
        _playerController.ChangeHealth(_health);
        PayForUpgrade();
        UpdateUIController();
    }

    private void OnUpgradeSpeed()
    {
        _speed += _speedUpgradeIncrement;
        _speedLevel++;
        _playerController.ChangeSpeed(_speed);
        PayForUpgrade();
        UpdateUIController();
    }

    public int IncrementCoins(int addedCoins)
    {
        _currentCoins += addedCoins;
        
        if(_currentCoins >= _upgradePrice)
            _uiController.EnableUpgrade();
        
        return _currentCoins;
    }

    public void UnsubscribeEvents()
    {
        _uiController.OnUpgradeDamageClick -= OnUpgradeDamage;
        _uiController.OnUpgradeHealthClick -= OnUpgradeHealth;
        _uiController.OnUpgradeSpeedClick -= OnUpgradeSpeed;
    }
}
