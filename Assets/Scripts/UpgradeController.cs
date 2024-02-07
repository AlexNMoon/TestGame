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
    private bool _isDamageTimerOn;
    private Coroutine _damageTimerCoroutine; 
    private bool _isSpeedTimerOn;
    private Coroutine _speedTimerCoroutine;

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
        _playerController.OnDamageBoosted += OnDamageBoosted;
        _playerController.OnSpeedBoosted += OnSpeedBoosted;
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

    private void OnDamageBoosted(int addPercent, int timer)
    {
        _damageBonus = Mathf.RoundToInt((_damage * addPercent) / 100);
        _playerController.ChangeDamage(_damage + _damageBonus);
        _uiController.ShowDamageBooster(_damage + _damageBonus, timer);
        
        if(_isDamageTimerOn)
            _playerController.StopCoroutine(_damageTimerCoroutine);
        
        _damageTimerCoroutine = _playerController.StartCoroutine(DamageTimer(timer));
    }

    private IEnumerator DamageTimer(int seconds)
    {
        _isDamageTimerOn = true;
        float timeRemaining = seconds;
        
        while (timeRemaining > 0)
        {
            yield return new WaitForEndOfFrame();
            timeRemaining -= Time.deltaTime;
        }
        
        _isDamageTimerOn = false;
        _playerController.ChangeDamage(_damage);
    }

    private void OnSpeedBoosted(int addPercent, int timer)
    {
        _speedBonus = Mathf.RoundToInt((_speed * addPercent) / 100);
        _playerController.ChangeSpeed(_speed + _speedBonus);
        _uiController.ShowSpeedBooster(_speed + _speedBonus, timer);
        
        if(_isSpeedTimerOn)
            _playerController.StopCoroutine(_speedTimerCoroutine);
        
        _speedTimerCoroutine = _playerController.StartCoroutine(SpeedTimer(timer));
    }
    
    private IEnumerator SpeedTimer(int seconds)
    {
        _isSpeedTimerOn = true;
        float timeRemaining = seconds;
        
        while (timeRemaining > 0)
        {
            yield return new WaitForEndOfFrame();
            timeRemaining -= Time.deltaTime;
        }
        
        _isSpeedTimerOn = false;
        _playerController.ChangeSpeed(_speed);
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
        _playerController.OnDamageBoosted -= OnDamageBoosted;
        _playerController.OnSpeedBoosted -= OnSpeedBoosted;
    }
}
