using UnityEngine;
using System.IO;
public class ScreenShot : MonoBehaviour
{
	// static reference to ScreenShot so can be called from other scripts directly (not just through gameobject component)
	public static ScreenShot screenShot;

	// The key used to take a screenshot
	public string screenShotKey = "k";

	// The amount to scale the screenshot
	public int scaleFactor = 1;

	private int screenshotCount = 0;

	// The key used to get/set the number of images
	private const string ImageCntKey = "IMAGE_CNT";

	void Awake()
	{
		if (screenShot != null)
		{ 
			// this gameobject must already have been setup in a previous scene, so just destroy this game object
			Destroy(this.gameObject);
		}
		else
		{ 
			// this is the first time we are setting up the screenshot utility
			// setup reference to ScreenshotUtility class
			screenShot = this.GetComponent<ScreenShot>();

			// keep this gameobject around as new scenes load
			DontDestroyOnLoad(gameObject);

			// get image count from player prefs for indexing of filename
			screenshotCount = PlayerPrefs.GetInt(ImageCntKey);
		}

		// if there is not a "Screenshots" directory in the Project folder, create one
		if (!Directory.Exists("Screenshots"))
		{
			Directory.CreateDirectory("Screenshots");
		}

	}
	void Update()
	{
		// Checks for input
		if (Input.GetKeyDown(screenShotKey.ToLower()))
		{
			// Saves the current image count
			PlayerPrefs.SetInt(ImageCntKey, ++screenshotCount);

			// Adjusts the height and width for the file name
			int width = Screen.width * scaleFactor;
			int height = Screen.height * scaleFactor;

			// Takes the screenshot with filename "Screenshot_WIDTHxHEIGHT_IMAGECOUNT.png"
			// and save it in the Screenshots folder
			ScreenCapture.CaptureScreenshot("Screenshots/Screenshot_" +
										  +width + "x" + height
										  + "_"
										  + screenshotCount
										  + ".png",
										  scaleFactor);
		}
	}
}
