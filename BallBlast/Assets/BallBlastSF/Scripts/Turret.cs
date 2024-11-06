using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float fireRate;
    [SerializeField] private int damage;
    [SerializeField] private int projectileAmount;
    [SerializeField] private float projectileInterval;

    public int Damage => damage;
    public int ProjectileAmount => projectileAmount;
    public float FireRate => fireRate;


    private float timer;


    void Update()
    {
        timer += Time.deltaTime;

    }

    private void SpawnProjecttile()
    {
        float startPosX = shootPoint.position.x - projectileInterval * (projectileAmount - 1) * 0.5f;

        for (int i = 0; i < projectileAmount; i++)
        {
            Instantiate(projectilePrefab, new Vector3(startPosX + i * projectileInterval, shootPoint.position.y, shootPoint.position.z), transform.rotation);
        }

        
    }

    public void Fire()
    {
        if (timer >= fireRate)
        {
            SpawnProjecttile();
            timer = 0;
        }
    }

}
