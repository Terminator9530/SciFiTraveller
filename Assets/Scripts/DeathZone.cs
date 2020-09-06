using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{

	public bool destroyNonPlayerObjects = true;

	// Handle gameobjects collider with a deathzone object
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (other.gameObject.GetComponent<PlayerControls>() != null)
			{
				// if player then tell the player to do its FallDeath
				other.gameObject.GetComponent<PlayerControls>().FallDeath();
			}

			if (other.gameObject.GetComponent<AeroplaneController>() != null)
			{
				// if player then tell the player to do its FallDeath
				other.gameObject.GetComponent<AeroplaneController>().FallDeath();
			}
		}
		else if (destroyNonPlayerObjects)
		{ // not playe so just kill object - could be falling enemy for example
			Destroy(other.gameObject);
		}
	}
}
