using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{

	public int coinValue = 1;
	public bool taken = false;
	public GameObject explosion;

	// if the player touches the coin, it has not already been taken, and the player can move (not dead or victory)
	// then take the coin
	void OnTriggerEnter2D(Collider2D other)
	{
		if ((other.tag == "Player") && (!taken))
		{
			if(other.gameObject.GetComponent<PlayerControls>() != null)
			{
				if (other.gameObject.GetComponent<PlayerControls>().playerCanMove)
				{
					TakeCoin(other);
				}
			}

			if(other.gameObject.GetComponent<AeroplaneController>() != null)
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
			// do the player collect coin thing
			other.gameObject.GetComponent<PlayerControls>().CollectCoin(coinValue);
		}

		if (other.gameObject.GetComponent<AeroplaneController>() != null)
		{
			// do the player collect coin thing
			other.gameObject.GetComponent<AeroplaneController>().CollectCoin(coinValue);
		}

		// destroy the coin
		Destroy(gameObject);
	}

}
