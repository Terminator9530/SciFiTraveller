using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

public class AeroplaneController : MonoBehaviour
{
    public GameObject player;
    public GameObject healthBar;
    public GameObject cameraObject;
    public float moveSpeed;
    private bool isShooting = false;
    public AudioClip shoot;
    public AudioClip fallSFX;
    private Animator _animator;
    public GameObject bullet;
    public GameObject bulletShoot;
    public float bulletForce;
    private AudioSource _audio;
    private bool facingRight = true;
    private Transform _transform;
    private float _vx;
    private float _vy;
    public AudioClip coinSFX;
    public AudioClip deathSFX;
    public AudioClip victorySFX;
    // player health
    public int playerHealth = 1;

    private Rigidbody2D _rigidbody;

    // player can move?
    // we want this public so other scripts can access it but we don't want to show in editor as it might confuse designer
    [HideInInspector]
    public bool playerCanMove = true;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // exit update if player cannot move or game is paused
        if (!playerCanMove || (Time.timeScale == 0f))
            return;

        _vx = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        _vy = CrossPlatformInputManager.GetAxisRaw("Vertical");

        // Change the actual velocity on the rigidbody
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_vx * moveSpeed, _vy * moveSpeed);

        if (CrossPlatformInputManager.GetButtonDown("Switch"))
        {
            player.SetActive(true);
            healthBar.GetComponent<Slider>().value = player.GetComponent<PlayerControls>().playerHealth;
            player.transform.position = gameObject.transform.position;
            cameraObject.GetComponent<CameraFollow>().target = player.transform;
            gameObject.SetActive(false);
        }

        // shooting
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            isShooting = true;
            PlaySound(shoot);
        }

        if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            isShooting = false;
        }

        _animator.SetBool("Shooting", isShooting);

        if (isShooting)
        {
            GameObject bulletInstance = Instantiate(bullet, bulletShoot.transform.position, bulletShoot.transform.rotation);
            if (facingRight)
                bulletInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletForce * 1, 0));
            else
                bulletInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletForce * -1, 0));
        }
    }

    // Checking to see if the sprite should be flipped
    // this is done in LateUpdate since the Animator may override the localScale
    // this code will flip the player even if the animator is controlling scale
    void LateUpdate()
    {
        // get the current scale
        Vector3 localScale = _transform.localScale;

        if (_vx > 0) // moving right so face right
        {
            facingRight = true;
        }
        else if (_vx < 0)
        { // moving left so face left
            facingRight = false;
        }

        // check to see if scale x is right for the player
        // if not, multiple by -1 which is an easy way to flip a sprite
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        // update the scale
        _transform.localScale = localScale;
    }

    // play sound through the audiosource on the gameobject
    void PlaySound(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

    public void CollectCoin(int amount)
    {
        PlaySound(coinSFX);

        if (GameManager.gm) // add the points through the game manager, if it is available
            GameManager.gm.AddPoints(amount);
    }

    // public function to respawn the player at the appropriate location
    public void Respawn(Vector3 spawnloc)
    {
        UnFreezeMotion();
        playerHealth = 100;
        playerHealth = 100;
        healthBar.GetComponent<Slider>().value = 100;
        _transform.parent = null;
        _transform.position = spawnloc;
        _animator.SetTrigger("Respawn");
    }

    // public function on victory over the level
    public void Victory()
    {
        PlaySound(victorySFX);
        FreezeMotion();
        _animator.SetTrigger("Victory");

        if (GameManager.gm) // do the game manager level compete stuff, if it is available
            GameManager.gm.LevelCompete();
    }

    // public function to apply damage to the player
    public void ApplyDamage(int damage)
    {
        if (playerCanMove)
        {
            playerHealth -= damage;
            healthBar.GetComponent<Slider>().value = playerHealth;
            if (playerHealth <= 0)
            { // player is now dead, so start dying
                PlaySound(deathSFX);
                StartCoroutine(KillPlayer());
            }
        }
    }

    // public function to kill the player when they have a fall death
    public void FallDeath()
    {
        if (playerCanMove)
        {
            playerHealth = 0;
            PlaySound(fallSFX);
            StartCoroutine(KillPlayer());
        }
    }

    // coroutine to kill the player
    IEnumerator KillPlayer()
    {
        if (playerCanMove)
        {
            // freeze the player
            FreezeMotion();

            // play the death animation
            _animator.SetTrigger("Death");

            // After waiting tell the GameManager to reset the game
            yield return new WaitForSeconds(2.0f);

            if (GameManager.gm) // if the gameManager is available, tell it to reset the game
                GameManager.gm.ResetGame();
            else // otherwise, just reload the current level
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // do what needs to be done to freeze the player
    void FreezeMotion()
    {
        playerCanMove = false;
        _rigidbody.velocity = new Vector2(0, 0);
    }

    // do what needs to be done to unfreeze the player
    void UnFreezeMotion()
    {
        playerCanMove = true;
    }
}
