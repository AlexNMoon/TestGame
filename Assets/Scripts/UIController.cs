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
}
