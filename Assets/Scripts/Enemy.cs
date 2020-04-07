using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathFX;
    public int hp = 5;

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameObject tempFX = Instantiate(deathFX, transform.position, Quaternion.identity);

        Destroy(tempFX, 1);
        Destroy(gameObject);
    }
}
