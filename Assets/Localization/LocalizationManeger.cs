using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LocalizationManeger : MonoBehaviour
{
    public void Ru() {
        string language = "Ru";
        PlayerPrefs.SetString("language", language);
    }
    public void Eng() {
        string language = "Eng";
        PlayerPrefs.SetString("language", language);
    }
}
