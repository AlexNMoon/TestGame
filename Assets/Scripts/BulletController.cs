using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody bulletRigidbody;

    private Transform _transform;
    private bool _isMoving;
    private string _targetTag;
    private int _damage;
    private int _speed;
    
    private const string BorderTag = "Border";

    private void Awake()
    {
        _transform = transform;
    }

    public void Init(int damage, string target, int speed)
    {
        _damage = damage;
        _targetTag = target;
        _speed = speed;
    }

    public void Move(Vector3 rotation)
    {
        _transform.rotation = Quaternion.Euler(rotation);
        _isMoving = true;
    }

    public void Reset(Vector3 position)
    {
        _transform.position = position;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if(!_isMoving) return;
        
        bulletRigidbody.position += _transform.up * ( Time.deltaTime * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(BorderTag))
        {
            gameObject.SetActive(false);
            _isMoving = false;
        }
    }
}
