using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private EnemyController enemyPrefab;
    [SerializeField] 
    private PlayerController playerPrefab;
    [SerializeField] 
    private Transform fieldTransform;
    [SerializeField] 
    private UIController uiController;
    [SerializeField] 
    private PlayerSettingsSO playerSettings;

    private PlayerController _player;
    private EnemyController _enemy;
    private Vector3 _playerStartPosition = new Vector3(0, 1, 0);
    private bool _onPause;
    private bool _gameOver;

    private void Awake()
    {
        InstantiatePlayer();
        InstantiateEnemy();
    }

    private void InstantiatePlayer()
    {
        _player = Instantiate(playerPrefab, _playerStartPosition, Quaternion.identity);
        _player.SetUp(playerSettings);
        _player.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        Time.timeScale = 0;
        uiController.ShowGameOver();
        _gameOver = true;
    }

    private void InstantiateEnemy()
    {
        _enemy = Instantiate(enemyPrefab, GetEnemyPosition(), Quaternion.identity);
        _enemy.OnEnemyDeath += OnEnemyDeath;
    }

    private void OnEnemyDeath()
    {
        _enemy.ActivateEnemy(GetEnemyPosition());
    }

    private Vector3 GetEnemyPosition()
    {
        var localScale = fieldTransform.localScale;
        
        float halfWidth = 5 * ((localScale.x - 0.5f) / 2);
        float x = Random.Range(-halfWidth, halfWidth);

        float halfHeight = 5 * ((localScale.z - 0.5f) / 2);
        float z = Random.Range(-halfHeight, halfHeight);
        
        return new Vector3(x, 1, z);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangeMenuVisibility();
    }

    private void ChangeMenuVisibility()
    {
        if (_gameOver) return;
        
        if(!_onPause)
        {
            Time.timeScale = 0;
            uiController.ShowUpgradeMenu(true);
            _onPause = true;
        }
        else
        {
            Time.timeScale = 1;
            uiController.ShowUpgradeMenu(false);
            _onPause = false;
        }
    }

    private void OnDestroy()
    {
        _enemy.OnEnemyDeath -= OnEnemyDeath;
        _player.OnPlayerDeath -= OnPlayerDeath;
    }
}
