using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject explosion;
    public float destroyTime = 2f;
    public float explosionRadius = 5f;
    public int damage = 5;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BlastBarrel();
        }
    }

    public void BlastBarrel()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerMask);
        foreach (Collider2D hitCollider in hitColliders)
        {
            Debug.Log("Collider : " + hitCollider.name);
            if (hitCollider.CompareTag("Enemy"))
                hitCollider.GetComponent<SpaceShip>().ApplyDamage(damage);

            if (hitCollider.CompareTag("Player"))
            {
                if (hitCollider.GetComponent<PlayerControls>() != null)
                {
                    hitCollider.GetComponent<PlayerControls>().ApplyDamage(damage);
                }

                if (hitCollider.GetComponent<AeroplaneController>() != null)
                {
                    hitCollider.GetComponent<AeroplaneController>().ApplyDamage(damage);
                }
            }
        }

        StartCoroutine(DestroyBarrel());
    }

    IEnumerator DestroyBarrel()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.75F);
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
