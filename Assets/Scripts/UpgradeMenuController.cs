using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UpgradeMenuController : MonoBehaviour
{
    public event Action OnUpgradeHealthClick;
    public event Action OnUpgradeSpeedClick;
    public event Action OnUpgradeDamageClick;
    
    [SerializeField] 
    private TMP_Text currentCoins;
    [SerializeField] 
    private TMP_Text currentVitality;
    [SerializeField] 
    private TMP_Text currentStrength;
    [SerializeField] 
    private TMP_Text currentDexterity;
    [SerializeField] 
    private TMP_Text currentVitalityLevel;
    [SerializeField] 
    private TMP_Text currentStrengthLevel;
    [SerializeField] 
    private TMP_Text currentDexterityLevel;
    [SerializeField] 
    private TMP_Text nextVitalityLevel;
    [SerializeField] 
    private TMP_Text nextStrengthLevel;
    [SerializeField] 
    private TMP_Text nextDexterityLevel;
    [SerializeField] 
    private Button upgradeVitalityButton;
    [SerializeField] 
    private Button upgradeStrengthButton;
    [SerializeField] 
    private Button upgradeDexterityButton;
    [SerializeField] 
    private TMP_Text upgradeVitalityPrice;
    [SerializeField] 
    private TMP_Text upgradeStrengthPrice;
    [SerializeField] 
    private TMP_Text upgradeDexterityPrice;

    private const string LevelText = "Lv ";

    public void SetUp(int coins, int health, int damage, int speed, int healthLevel, int damageLevel,
        int speedLevel, int upgradePrice, bool upgradeButtonsInteractivity)
    {
        currentCoins.text = coins.ToString();
        currentVitality.text = health.ToString();
        currentStrength.text = damage.ToString();
        currentDexterity.text = speed.ToString();
        currentVitalityLevel.text = LevelText + healthLevel;
        currentStrengthLevel.text = LevelText + damageLevel;
        currentDexterityLevel.text = LevelText + speedLevel;
        nextVitalityLevel.text = LevelText + (healthLevel + 1);
        nextStrengthLevel.text = LevelText + (damageLevel + 1);
        nextDexterityLevel.text = LevelText + (speedLevel + 1);
        upgradeVitalityPrice.text = upgradePrice.ToString();
        upgradeStrengthPrice.text = upgradePrice.ToString();
        upgradeDexterityPrice.text = upgradePrice.ToString();
        EnableUpgradeButtons(upgradeButtonsInteractivity);
    }

    public void ChangeCoins(int coins)
    {
        currentCoins.text = coins.ToString();
    }

    public void EnableUpgradeButtons(bool enable)
    {
        upgradeVitalityButton.interactable = enable;
        upgradeStrengthButton.interactable = enable;
        upgradeDexterityButton.interactable = enable;
    }

    private void OnEnable()
    {
        upgradeVitalityButton.onClick.AddListener(OnUpgradeHealthButtonClick);
        upgradeDexterityButton.onClick.AddListener(OnUpgradeSpeedButtonClick);
        upgradeStrengthButton.onClick.AddListener(OnUpgradeDamageButtonClick);
    }

    private void OnUpgradeHealthButtonClick()
    {
        OnUpgradeHealthClick?.Invoke();
    }

    private void OnUpgradeSpeedButtonClick()
    {
        OnUpgradeSpeedClick?.Invoke();
    }

    private void OnUpgradeDamageButtonClick()
    {
        OnUpgradeDamageClick?.Invoke();
    }

    private void OnDisable()
    {
        upgradeVitalityButton.onClick.RemoveListener(OnUpgradeHealthButtonClick);
        upgradeDexterityButton.onClick.RemoveListener(OnUpgradeSpeedButtonClick);
        upgradeStrengthButton.onClick.RemoveListener(OnUpgradeDamageButtonClick);
    }
}
