using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualJoystickManager : MonoBehaviour
{
    public GameObject virtualJoystick;
    public GameObject pauseMenuButton;
    // Start is called before the first frame update
    void Start()
    {
		showJoystickWhenAppropriate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ShowJoystick()
	{
		virtualJoystick.SetActive(true);
		pauseMenuButton.SetActive(true);
	}

	public void HideJoystick()
	{
		virtualJoystick.SetActive(false);
		pauseMenuButton.SetActive(false);
	}

	private void showJoystickWhenAppropriate()
	{
		Debug.Log(Application.platform);
		switch (Application.platform)
		{
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.LinuxPlayer:
			case RuntimePlatform.WebGLPlayer:
				HideJoystick();
				Debug.Log("PC");
				break;

			case RuntimePlatform.Android:
			case RuntimePlatform.IPhonePlayer:
				ShowJoystick();
				Debug.Log("Android");
				break;

			default:
				Debug.Log("No plat");
				break;
		}
	}
}
