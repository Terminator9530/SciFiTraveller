using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamageToPlayer : MonoBehaviour
{
    public GameObject enemyController;
    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = enemyController.GetComponent<EnemyController>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyController.GetComponent<EnemyController>().ApplyDamageToPlayer(damage);
        }
    }
}
