using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class SecretCodeInputManager : MonoBehaviour
{
    private string enteredCode = "";
    public GameObject activateSwitch;
    public GameObject secretCard;
    public Sprite greenButtonSprite;
    public Sprite greyButtonSprite;
    public GameObject[] codeFieldsButtons;
    public GameObject[] codeFieldsText;
    public Animator status;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AppendCode(string txt)
    {
        if(enteredCode.Length < 4)
        {
            enteredCode += txt;
            codeFieldsText[enteredCode.Length - 1].GetComponent<Text>().text = txt;
            codeFieldsButtons[enteredCode.Length - 1].gameObject.GetComponent<UnityEngine.UI.Image>().sprite = greenButtonSprite;
        }
    }

    public void DeleteLastChar()
    {
        if(enteredCode != "")
        {
            codeFieldsText[enteredCode.Length - 1].GetComponent<Text>().text = "";
            codeFieldsButtons[enteredCode.Length - 1].gameObject.GetComponent<UnityEngine.UI.Image>().sprite = greyButtonSprite;
            enteredCode = enteredCode.Remove(enteredCode.Length - 1);
        }
    }

    public void CheckCode()
    {
        if(secretCard.GetComponent<SecretCard>().secretCode.ToString() == enteredCode)
        {
            activateSwitch.GetComponent<ActivateSwitch>().ActivateDoor();
            ClearCode();
            status.SetInteger("Status", 1);
            StartCoroutine(DisableGameObject());
        }
        else
        {
            status.SetInteger("Status", 2);
        }
    }

    public void ClearCode()
    {
        enteredCode = "";
        for (int i = 0;i< codeFieldsButtons.Length; i++){
            codeFieldsButtons[i].gameObject.GetComponent<UnityEngine.UI.Image>().sprite = greyButtonSprite;
            codeFieldsText[i].GetComponent<Text>().text = "";
        }
    }

    IEnumerator DisableGameObject()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        GameManager.gm.GetComponent<VirtualJoystickManager>().ShowJoystick();

    }
}
