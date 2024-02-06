using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ITarget
{
    [SerializeField] 
    private BulletController bulletPrefab;

    private List<BulletController> _bulletsPool;
    private int _damage = 20;
    private float _bulletSpeed = 10;
    private int _healthMax = 30;
    private int _healthCurrent = 30;
    private const string TargetTag = "Player";

    public void ReceiveDamage(int damage)
    {
        _healthCurrent -= damage;
        
        if(_healthCurrent <= 0)
            Debug.Log("Enemy dead!");
    }
    
    private void Start()
    {
        _bulletsPool = new List<BulletController>();
        StartCoroutine(WaitToShootBullet());
    }

    private IEnumerator WaitToShootBullet()
    {
        ShootBullet();
        yield return new WaitForSeconds(1);
        StartCoroutine(WaitToShootBullet());
    }

    private void ShootBullet()
    {
        BulletController bullet = GetAvailableBullet();
        bullet.Move(GetBulletTargetRotation());
    }

    private BulletController GetAvailableBullet()
    {
        BulletController bullet = _bulletsPool.Find(x => !x.gameObject.activeInHierarchy);

        if (bullet == null)
        {
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)), transform);
            bullet.Init(_damage, TargetTag, _bulletSpeed);
            _bulletsPool.Add(bullet);
        }
        else
        {
            bullet.Reset(transform.position);
        }

        return bullet;
    }

    private Vector3 GetBulletTargetRotation()
    {
        float y = Random.Range(0, 360);
        Vector3 rotation = new Vector3(0, y, 90);
        return rotation;
    }
}