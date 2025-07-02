using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreDisplay : UIelement
{
    public TMP_Text displayText = null;

    public void DisplayHighScore()
    {
        if (displayText != null)
        {
            displayText.SetText("High: " + GameManager.instance.highScore.ToString());
        }
    }

    public override void UpdateUI()
    {
        // This calls the base update UI function from the UIelement class
        base.UpdateUI();

        // The remaining code is only called for this sub-class of UIelement and not others
        DisplayHighScore();
    }
}
