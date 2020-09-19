using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class OpenDoor : MonoBehaviour
{
    public GameObject canvas;
    public GameObject doorUnlocked;
    public GameObject doorOpen;
    private bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered && CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            doorOpen.SetActive(true);
            doorUnlocked.SetActive(false);
            TasksManager.tm.TaskCompleted(2);
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
