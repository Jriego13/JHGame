using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public Rigidbody2D bullet;
    public int damage = 1;
    public float bulletSpeed = 2f;

    void Start()
    {
        bullet.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemyHit = collision.GetComponent<Enemy>(); // getting the enemy that got hit 
        if (enemyHit!= null)
        {
            enemyHit.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
