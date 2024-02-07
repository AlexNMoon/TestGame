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

    public void SetUp(int coinsAmount, int currentHealthAmount, int healthAmount, int strengthAmount, int dexterityAmount)
    {
        coins.text = coinsAmount.ToString();
        currentHealth.text = currentHealthAmount.ToString();
        health.text = healthAmount.ToString();
        strength.text = strengthAmount.ToString();
        dexterity.text = dexterityAmount.ToString();
    }

    public void SetCoins(int coinsAmount)
    {
        coins.text = coinsAmount.ToString();
    }

    public void ChangeCurrentHealth(int healthAmount)
    {
        currentHealth.text = healthAmount.ToString();
    }
}
