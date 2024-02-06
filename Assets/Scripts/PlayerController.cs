using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, ITarget
{
    [SerializeField] 
    private Rigidbody playerRigidbody;
    
    private float _speed = 4.0f;
    private int _healthMax = 100;
    private int _healthCurrent = 100;
    private Transform _transform;

    public void ReceiveDamage(int damage)
    {
        _healthCurrent -= damage;
        
        if(_healthCurrent <= 0)
            Debug.Log("Player dead!");
    }
    
    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        var position = playerRigidbody.position;
        position += _transform.forward * (z * Time.deltaTime * _speed);
        position += _transform.right * (x * Time.deltaTime * _speed);
        playerRigidbody.position = position;
    }
}
