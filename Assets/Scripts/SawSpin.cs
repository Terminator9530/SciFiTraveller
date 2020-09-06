using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSpin : MonoBehaviour
{
    private Transform saw;
    private int angle = 0;
    public int damage = 1;
    private bool isDamaging = false;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        saw = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        saw.transform.Rotate(saw.rotation.x, saw.rotation.y, angle, Space.Self);
        angle += 3;
        if (isDamaging)
        {
            if(player.GetComponent<PlayerControls>() != null)
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
        if(collision.gameObject.tag == "Player")
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
