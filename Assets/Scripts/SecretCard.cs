using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using Random = UnityEngine.Random;

public class SecretCard : MonoBehaviour
{
    public GameObject code;
    public int secretCode;
    public GameObject status;
    private bool isTriggered = false;
    public float revealingTime = 10f;
    public GameObject slider;
    private float startTime;
    private float endTime;
    private bool keyDown = false;
    private bool isFound = false;
    // Start is called before the first frame update
    void Start()
    {
        secretCode = Random.Range(1000, 10000);
        status.GetComponent<Text>().text = "Hold E to Activate";
    }

    // Update is called once per frame
    void Update()
    {
        if(isTriggered && startTime <= endTime)
        {
            if (CrossPlatformInputManager.GetButtonDown("Interact"))
            {
                startTime = 0;
                endTime = startTime + revealingTime;
                keyDown = true;
                status.GetComponent<Text>().text = "Activating";
                status.SetActive(true);
            }
            if (CrossPlatformInputManager.GetButtonUp("Interact"))
            {
                slider.GetComponent<Slider>().value = 0;
                status.GetComponent<Text>().text = "Hold E to Activate";
                keyDown = false;
            }
            if (keyDown)
            {
                startTime += Time.deltaTime;
                int percentage = (int) Mathf.Round((startTime / endTime) * 100);
                if(percentage <= 100)
                slider.GetComponent<Slider>().value = Mathf.Round((startTime / endTime) * 100);
            }
        }

        if(!isFound && startTime > endTime)
        {
            isFound = true;
            TasksManager.tm.TaskCompleted(0);
            status.GetComponent<Text>().text = "Activated";
            code.SetActive(true);
            code.GetComponent<Text>().text = secretCode.ToString();
            slider.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggered = true;
        status.SetActive(true);
        if (!isFound)
        slider.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;
        slider.SetActive(false);
        status.SetActive(false);
    }

    public void OpenCard(GameObject cardPanel)
    {
        if (isFound)
        {
            cardPanel.SetActive(true);
            GameObject secretCodeText = cardPanel.transform.GetChild(1).gameObject;
            secretCodeText.GetComponent<Text>().text = secretCode.ToString();
            GameManager.gm.PauseGame(true);
        }
    }
}
