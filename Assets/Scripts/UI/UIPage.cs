using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPage : MonoBehaviour
{
    [Tooltip("The default UI to have selected when opening this page")]
    public GameObject defaultSelected;

    public void SetSelectedUIToDefault()
    {
        if (UIManager.instance != null && defaultSelected != null)
        {
            UIManager.instance.eventSystem.SetSelectedGameObject(null);
            UIManager.instance.eventSystem.SetSelectedGameObject(defaultSelected);
        }
        
    }
}
