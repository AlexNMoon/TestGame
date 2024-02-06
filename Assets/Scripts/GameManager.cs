using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private EnemyController _enemyPrefab;
    [SerializeField] 
    private PlayerController _playerPrefab;
    [SerializeField] 
    private Transform _fieldTransform;

    private PlayerController _player;
    private EnemyController _enemy;
    private Vector3 _playerStartPosition = new Vector3(0, 1, 0);

    private void Awake()
    {
        InstantiatePlayer();
        InstantiateEnemy();
    }

    private void InstantiatePlayer()
    {
        _player = Instantiate(_playerPrefab, _playerStartPosition, Quaternion.identity);
    }

    private void InstantiateEnemy()
    {
        _enemy = Instantiate(_enemyPrefab, GetEnemyPosition(), Quaternion.identity);
        _enemy.OnEnemyDeath += OnEnemyDeath;
    }

    private void OnEnemyDeath()
    {
        _enemy.ActivateEnemy(GetEnemyPosition());
    }

    private Vector3 GetEnemyPosition()
    {
        var localScale = _fieldTransform.localScale;
        
        float halfWidth = 5 * ((localScale.x - 0.5f) / 2);
        float x = Random.Range(-halfWidth, halfWidth);

        float halfHeight = 5 * ((localScale.z - 0.5f) / 2);
        float z = Random.Range(-halfHeight, halfHeight);
        
        return new Vector3(x, 1, z);
    }

    private void OnDestroy()
    {
        _enemy.OnEnemyDeath -= OnEnemyDeath;
    }
}
