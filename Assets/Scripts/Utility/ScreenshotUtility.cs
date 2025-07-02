using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.InputSystem;

public class ScreenshotUtility : MonoBehaviour
{
    public static ScreenshotUtility screenShotUtility;

    #region Public Variables
    public bool runOnlyInEditor = true;
    public string m_ScreenshotKey = "c";
    public int m_ScaleFactor = 1;
    public bool includeImageSizeInFilename = true;
    #endregion

    #region Private Variables
    // The number of screenshots taken
    private int m_ImageCount = 0;
    #endregion

    #region Constants
    // The key used to get/set the number of images
    private const string ImageCntKey = "IMAGE_CNT";
    #endregion

    void Awake()
    {
        if (screenShotUtility != null)
        { // this gameobject must already have been setup in a previous scene, so just destroy this game object
            Destroy(this.gameObject);
        }
        else if (runOnlyInEditor && !Application.isEditor)
        { // chose not to work if this is running outside the editor so destroy it
            Destroy(this.gameObject);
        }
        else
        { // this is the first time we are setting up the screenshot utility
          // setup reference to ScreenshotUtility class
            screenShotUtility = this.GetComponent<ScreenshotUtility>();

            // keep this gameobject around as new scenes load
            DontDestroyOnLoad(gameObject);

            // get image count from player prefs for indexing of filename
            m_ImageCount = PlayerPrefs.GetInt(ImageCntKey);

            // if there is not a "Screenshots" directory in the Project folder, create one
            if (!Directory.Exists("Screenshots"))
            {
                Directory.CreateDirectory("Screenshots");
            }
        }
    }

    void Update()
    {
        if (Keyboard.current.FindKeyOnCurrentKeyboardLayout(m_ScreenshotKey).wasPressedThisFrame)
        {
            TakeScreenshot();
        }
    }

    public void ResetCounter()
    {
        // reset counter to 0
        m_ImageCount = 0;
        // set player prefs to new value
        PlayerPrefs.SetInt(ImageCntKey, m_ImageCount);
    }

    public void TakeScreenshot()
    {
        // Saves the current image count
        PlayerPrefs.SetInt(ImageCntKey, ++m_ImageCount);

        // Adjusts the height and width for the file name
        int width = Screen.width * m_ScaleFactor;
        int height = Screen.height * m_ScaleFactor;

        // Determine the pathname/filename for the file
        string pathname = "Screenshots/Screenshot_";
        if (includeImageSizeInFilename)
        {
            pathname += width + "x" + height + "_";
        }
        pathname += m_ImageCount + ".png";

        // Take the actual screenshot and save it
        ScreenCapture.CaptureScreenshot(pathname, m_ScaleFactor);
        Debug.Log("Screenshot captured at " + pathname);
    }
}
