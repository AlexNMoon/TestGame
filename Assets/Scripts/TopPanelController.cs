using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPanelController : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text coins;
    [SerializeField] 
    private TMP_Text currentHealth;
    [SerializeField] 
    private TMP_Text health;
    [SerializeField] 
    private TMP_Text strength;
    [SerializeField] 
    private TMP_Text strengthTimer;
    [SerializeField] 
    private TMP_Text dexterity;
    [SerializeField] 
    private TMP_Text dexterityTimer;

    private bool _isStrengthTimerOn;
    private Coroutine _strengthTimerCoroutine; 
    private bool _isDexterityTimerOn;
    private Coroutine _dexterityTimerCoroutine;
    private int _strengthAmount;
    private int _dexterityAmount;

    public void SetUp(int coinsAmount, int healthAmount, int damageAmount, int speedAmount, int? currentHealthAmount = null)
    {
        coins.text = coinsAmount.ToString();
        health.text = healthAmount.ToString();
        strength.text = damageAmount.ToString();
        _strengthAmount = damageAmount;
        dexterity.text = speedAmount.ToString();
        _dexterityAmount = speedAmount;
        
        if(currentHealthAmount != null)
            currentHealth.text = currentHealthAmount.ToString();

        if (_isStrengthTimerOn)
        {
            _isStrengthTimerOn = false;
            strengthTimer.text = String.Empty;
            StopCoroutine(_strengthTimerCoroutine);
        }

        if (_isDexterityTimerOn)
        {
            _isDexterityTimerOn = false;
            dexterityTimer.text = String.Empty;
            StopCoroutine(_dexterityTimerCoroutine);
        }
    }

    public void SetCoins(int coinsAmount)
    {
        coins.text = coinsAmount.ToString();
    }

    public void ChangeCurrentHealth(int healthAmount)
    {
        currentHealth.text = healthAmount.ToString();
    }

    public void ShowDamageBooster(int damageAmount, int timer)
    {
        strength.text = damageAmount.ToString();
        
        if(_isStrengthTimerOn)
            StopCoroutine(_strengthTimerCoroutine);
        
        _strengthTimerCoroutine = StartCoroutine(DamageTimer(timer));
    }

    private IEnumerator DamageTimer(int seconds)
    {
        _isStrengthTimerOn = true;
        float timeRemaining = seconds;
        
        while (timeRemaining > 0)
        {
            float min = Mathf.FloorToInt(timeRemaining / 60);
            float sec = Mathf.FloorToInt(timeRemaining % 60);
            strengthTimer.text = string.Format("{0:00}:{1:00}", min, sec);
            yield return new WaitForEndOfFrame();
            timeRemaining -= Time.deltaTime;
        }
        
        strengthTimer.text = String.Empty;
        _isStrengthTimerOn = false;
        strength.text = _strengthAmount.ToString();
    }

    public void ShowSpeedBooster(int speedAmount, int timer)
    {
        dexterity.text = speedAmount.ToString();
        
        if(_isDexterityTimerOn)
            StopCoroutine(_dexterityTimerCoroutine);
        
        _dexterityTimerCoroutine = StartCoroutine(SpeedTimer(timer));
    }

    private IEnumerator SpeedTimer(int seconds)
    {
        _isDexterityTimerOn = true;
        float timeRemaining = seconds;
        
        while (timeRemaining > 0)
        {
            float min = Mathf.FloorToInt(timeRemaining / 60);
            float sec = Mathf.FloorToInt(timeRemaining % 60);
            dexterityTimer.text = string.Format("{0:00}:{1:00}", min, sec);
            yield return new WaitForEndOfFrame();
            timeRemaining -= Time.deltaTime;
        }
        
        dexterityTimer.text = String.Empty;
        _isDexterityTimerOn = false;
        dexterity.text = _dexterityAmount.ToString();
    }
}
