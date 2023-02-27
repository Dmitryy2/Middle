using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManeg : MonoBehaviour
{
    //public GameObject settingsPanel;
    public void PlayGame() {
        Application.LoadLevel("Levl1");
    }
    public void ExitGame() {
        Application.Quit();
    }
}
