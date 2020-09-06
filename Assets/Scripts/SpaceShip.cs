using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    public int damage = 1;
    private bool isDamaging = false;
    private GameObject player;
    public int health = 10;
    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamaging)
        {
            if (player.GetComponent<PlayerControls>() != null)
            {
                player.GetComponent<PlayerControls>().ApplyDamage(damage);
            }

            if (player.GetComponent<AeroplaneController>() != null)
            {
                player.GetComponent<AeroplaneController>().ApplyDamage(damage);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isDamaging = true;
            player = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isDamaging = false;
        }
    }

    // public function to apply damage to the player
    public void ApplyDamage(int damage)
    {
        health -= damage;
        healthBar.GetComponent<Slider>().value = health;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
