using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float radius = 10f;
    public LayerMask layerMask;
    public int damage = 1;
    private bool isDamaging = false;
    private GameObject player;
    public int health = 50;
    public Slider healthBar;
    public GameObject enemyObject;
    public GameObject endWayPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
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

        if(gameObject.transform.position == endWayPoint.transform.position)
        {
            GetComponent<Animator>().SetBool("Glide",false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Attack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        if(hitColliders.Length == 0)
        {
            GetComponent<Animator>().SetBool("Attacking", false);
            return;
        }
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                GetComponent<Animator>().SetBool("Attacking", true);
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

        if(health <= 50)
        {
            GetComponent<Animator>().SetBool("Glide",true);
            enemyObject.GetComponent<PlatformMover>().enabled = true;
        }
    }
}
