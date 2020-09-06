using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage = 1;
    private bool isDamaging = false;
    private GameObject player;
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
}
