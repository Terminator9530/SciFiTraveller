using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SecretCard : MonoBehaviour
{
    public GameObject code;
    public int secretCode;
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
    }

    // Update is called once per frame
    void Update()
    {
        if(isTriggered && startTime <= endTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                startTime = 0;
                endTime = startTime + revealingTime;
                keyDown = true;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                slider.GetComponent<Slider>().value = 0;
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
            code.SetActive(true);
            code.GetComponent<Text>().text = secretCode.ToString();
            slider.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggered = true;
        slider.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;
        slider.SetActive(false);
    }

    public void OpenCard(GameObject cardPanel)
    {
        if (isFound)
        {
            cardPanel.SetActive(true);
            GameObject secretCodeText = cardPanel.transform.GetChild(1).gameObject;
            secretCodeText.GetComponent<Text>().text = secretCode.ToString();
        }
    }
}
