using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect; 
    public float healt =4f;

    public static int EnemiesAlive = 0;


    private void Start()
    {
        EnemiesAlive++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�arp��an iki nesne aras�ndaki h�z� temsil eder.
        if (collision.relativeVelocity.magnitude>healt)
        {
            Die();
        }

    }

   void Die()
    {
        Instantiate(deathEffect, transform.position,Quaternion.identity);
        
        EnemiesAlive--;
        if (EnemiesAlive <= 0)
            Debug.Log("Level Won!");
        
        Destroy(gameObject);
    }

}
