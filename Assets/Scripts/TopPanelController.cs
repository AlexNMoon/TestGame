using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPanelController : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text coins;
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

    private int _healthMax;
    private int _currentHealth;

    public void SetUp()
    {
        coins.text = "0";
    }

    public void SetCoins(int coinsAmount)
    {
        coins.text = coinsAmount.ToString();
    }
}
