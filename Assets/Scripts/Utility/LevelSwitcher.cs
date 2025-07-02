using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        LevelManager.LoadScene(sceneName);
    }
}
