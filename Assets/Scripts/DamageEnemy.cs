﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public int damage = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if(collision.gameObject.GetComponent<SpaceShip>() != null)
            collision.gameObject.GetComponent<SpaceShip>().ApplyDamage(damage);

            if (collision.gameObject.GetComponent<EnemyController>() != null)
            collision.gameObject.GetComponent<EnemyController>().ApplyDamage(damage);

            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Barrel")
        {
            collision.gameObject.GetComponent<Barrel>().BlastBarrel();
        }
    }
}
