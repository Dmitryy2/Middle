using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Application.LoadLevel("Levl1");
    }
    public void ExitMenu()
    {
        Application.LoadLevel("_Main");
    }
}
