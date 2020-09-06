using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour
{

	public bool taken = false;
	public GameObject explosion;

	// if the player touches the victory object, it has not already been taken, and the player can move (not dead or victory)
	// then the player has reached the victory point of the level
	void OnTriggerEnter2D(Collider2D other)
	{
		if ((other.tag == "Player") && (!taken))
		{
			if (other.gameObject.GetComponent<PlayerControls>() != null)
			{
				if (other.gameObject.GetComponent<PlayerControls>().playerCanMove)
				{
					TakeCoin(other);
				}
			}

			if (other.gameObject.GetComponent<AeroplaneController>() != null)
			{
				if (other.gameObject.GetComponent<AeroplaneController>().playerCanMove)
				{
					TakeCoin(other);
				}
			}
		}
	}

	private void TakeCoin(Collider2D other)
	{
		// mark as taken so doesn't get taken multiple times
		taken = true;

		// if explosion prefab is provide, then instantiate it
		if (explosion)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.gameObject.GetComponent<PlayerControls>() != null)
		{
			// do the player victory thing
			other.gameObject.GetComponent<PlayerControls>().Victory();
		}

		if (other.gameObject.GetComponent<AeroplaneController>() != null)
		{
			// do the player victory thing
			other.gameObject.GetComponent<AeroplaneController>().Victory();
		}

		// destroy the coin
		Destroy(gameObject);
	}

}
