using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // public variables
    public float radius = 10f;
    public LayerMask layerMask;
    public int damage = 1;
    public int health = 50;
    public Slider healthBar;
    public GameObject enemyObject;
    public GameObject endWayPoint;
    
    //private variables
    private bool isDamaging = false;
    private GameObject player;
    private bool inRadius = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceTowardsPlayer();
        Attack();
        if(isDamaging)
        ApplyDamageToPlayer(damage);
        StopGlide();
    }

    // draw sphere gizmos on selecting gameobject
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    // find player if it is withing enemy radius
    public void Attack()
    {
        inRadius = false;
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
                player = hitCollider.gameObject;
                inRadius = true;
            }
        }
    }

    // if player collide with enemy then apply damage
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

    // public function to apply damage to the enemy
    public void ApplyDamage(int damage)
    {
        health -= damage;
        healthBar.GetComponent<Slider>().value = health;

        // there is no health then enemy will die
        if (health <= 0)
        {
            Destroy(enemyObject);
        }

        // if enemy health is below 50 then it will glide
        if(health <= 50)
        {
            StartGliding();
        }
    }

    private void StartGliding()
    {
        GetComponent<Animator>().SetBool("Glide", true);
        enemyObject.GetComponent<PlatformMover>().enabled = true;
    }

    // update scale to make enemy face towards player
    private void FaceTowardsPlayer()
    {
        if (inRadius && gameObject.transform.position.x > player.transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(-1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
    }

    // apply damage to player on attack
    public void ApplyDamageToPlayer(int damage)
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

    // stop gliding on reaching end waypoint
    private void StopGlide()
    {
        if (gameObject.transform.position == endWayPoint.transform.position)
        {
            GetComponent<Animator>().SetBool("Glide", false);
        }
    }
}
