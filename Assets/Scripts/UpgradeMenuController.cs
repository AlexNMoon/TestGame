using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UpgradeMenuController : MonoBehaviour
{
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

    public void SetUp(PlayerSettingsSO settings)
    {
        currentCoins.text = "0";
        currentVitality.text = settings.Health.ToString();
        currentStrength.text = settings.Damage.ToString();
        currentDexterity.text = settings.Speed.ToString();
        currentVitalityLevel.text = LevelText + "1";
        currentStrengthLevel.text = LevelText + "1";
        currentDexterityLevel.text = LevelText + "1";
        nextVitalityLevel.text = LevelText + "2";
        nextStrengthLevel.text = LevelText + "2";
        nextDexterityLevel.text = LevelText + "2";
        upgradeVitalityPrice.text = settings.FirstUpgradePrice.ToString();
        upgradeStrengthPrice.text = settings.FirstUpgradePrice.ToString();
        upgradeDexterityPrice.text = settings.FirstUpgradePrice.ToString();
        upgradeVitalityButton.interactable = false;
        upgradeStrengthButton.interactable = false;
        upgradeDexterityButton.interactable = false;
    }

    public void ChangeCoins(int coins)
    {
        currentCoins.text = coins.ToString();
    }
    
}
