using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSwitch : MonoBehaviour
{
    public GameObject canvas;
    public GameObject activatedSwitch;
    public GameObject doorLocked;
    public GameObject doorUnlocked;
    private bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.E))
        {
            activatedSwitch.SetActive(true);
            doorLocked.SetActive(false);
            doorUnlocked.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = false;
            canvas.SetActive(false);
        }
    }
}
