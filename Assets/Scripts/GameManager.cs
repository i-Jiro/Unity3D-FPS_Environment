using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Called by Unity event in Pause Menu UI.
    public void QuitGame()
    {
        Application.Quit();
    }
}
