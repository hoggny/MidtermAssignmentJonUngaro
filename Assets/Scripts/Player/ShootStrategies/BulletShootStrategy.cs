using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootStrategy : IShootStrategy
{
    ShootInteractor _interactor;
    Transform _spawnPoint;

    //Constructor 
    public BulletShootStrategy(ShootInteractor interactor)
    {
        Debug.Log("Switched to Bullet Shooting Mode!");

        _interactor = interactor;
        _spawnPoint = interactor.GetSpawnPoint();

        //Change Gun Color
        _interactor.gunRenderer.material.color = _interactor.bulletGunColor;
    }
    public void Shoot()
    {
        PooledObject pooledBullet = _interactor.bulletPool.GetPooledObject();
        pooledBullet.gameObject.SetActive(true);

        Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();

        bullet.transform.position = _spawnPoint.position;
        bullet.transform.rotation = _spawnPoint.rotation;

        //bullet.AddForce(_spawnPoint.forward * _finalShootVelocity, ForceMode.Impulse);
        bullet.velocity = _spawnPoint.forward * _interactor.GetFinalShootVelocity();

        //Rigidbody bulletRb = Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
        //bulletRb.AddForce(_spawnPoint.forward * _finalShootVelocity, ForceMode.Impulse);

        //Destroy(bulletRb.gameObject, 5f);
        _interactor.bulletPool.DestroyPooledObject(pooledBullet, 1.0f);
    }

}
