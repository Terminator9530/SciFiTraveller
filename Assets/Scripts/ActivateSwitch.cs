using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ActivateSwitch : MonoBehaviour
{
    public GameObject canvas;
    public GameObject activatedSwitch;
    public GameObject doorLocked;
    public GameObject doorUnlocked;
    private bool isTriggered = false;
    public GameObject secretCodeInputPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered && CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            secretCodeInputPanel.SetActive(true);
            GameManager.gm.PauseGame(true);
            GameManager.gm.GetComponent<VirtualJoystickManager>().HideJoystick();
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

    public void ActivateDoor()
    {
        activatedSwitch.SetActive(true);
        doorLocked.SetActive(false);
        doorUnlocked.SetActive(true);
        gameObject.SetActive(false);
    }
}
