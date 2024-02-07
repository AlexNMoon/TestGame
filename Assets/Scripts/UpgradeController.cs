using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController
{
    private int _speed;
    private int _speedBonus;
    private int _health;
    private int _damage ;
    private int _damageBonus;
    private int _speedUpgradeIncrement;
    private int _healthUpgradeIncrement;
    private int _damageUpgradeIncrement;
    private int _upgradePrice;
    private int _upgradePriceIncrement;
    private int _currentCoins;

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

    public int IncrementCoins(int addedCoins)
    {
        return _currentCoins += addedCoins;
    }
}
