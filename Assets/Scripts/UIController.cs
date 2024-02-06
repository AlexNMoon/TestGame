using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] 
    private GameObject background;
    [SerializeField] 
    private GameObject gameOverPanel;

    public void ShowGameOver()
    {
        background.SetActive(true);
        gameOverPanel.SetActive(true);
    }
}
