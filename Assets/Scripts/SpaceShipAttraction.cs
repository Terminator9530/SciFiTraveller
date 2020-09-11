using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAttraction : MonoBehaviour
{
    public Rigidbody2D attractingObject;
    private Rigidbody2D attractedObject;
    private bool isAttraction = false;
    public float G = 0.05f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAttraction)
        {
            AttractObject();
            if(attractedObject.transform.position.y >= attractingObject.transform.position.y)
            {
                DestroyPlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collider : " + collision);
        if(collision.tag == "Player")
        {
            isAttraction = true;
            attractedObject = collision.GetComponent<Rigidbody2D>();
            attractingObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public void AttractObject()
    {
        attractedObject.gravityScale = 0;
        Vector2 directionVector = attractingObject.position - attractedObject.position;
        float magnitude = directionVector.magnitude;
        float forceMag = G * (attractedObject.mass * attractingObject.mass) / (Mathf.Pow(magnitude, 2));
        Debug.Log("Force mag : " + forceMag);
        Vector3 force = directionVector * forceMag;
        Debug.Log("Force : " + force);
        attractedObject.AddForce(force);
    }

    public void DestroyPlayer()
    {
        Destroy(attractedObject.gameObject);
        isAttraction = false;
        Debug.Log("Destroyed");
    }
}
