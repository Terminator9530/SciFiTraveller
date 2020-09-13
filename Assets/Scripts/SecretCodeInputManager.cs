using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCodeInputManager : MonoBehaviour
{
    private string enteredCode = "";
    public GameObject activateSwitch;
    public GameObject secretCard;
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
        enteredCode += txt;
    }

    public void DeleteLastChar()
    {
        if(enteredCode != "")
        enteredCode = enteredCode.Remove(enteredCode.Length - 1);
    }

    public void CheckCode()
    {
        if(secretCard.GetComponent<SecretCard>().secretCode.ToString() == enteredCode)
        {
            activateSwitch.GetComponent<ActivateSwitch>().ActivateDoor();
            ClearCode();
            gameObject.SetActive(false);
        }
    }

    public void ClearCode()
    {
        enteredCode = "";
    }
}
