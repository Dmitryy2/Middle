using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextLanguage : MonoBehaviour
{
    public string language;
    Text text;

    public string textRu;
    public string textEng;

    void Start()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        language = PlayerPrefs.GetString("language");
        if (language == "" || language == "Ru") {
            text.text = textRu;
        }
        else if (language == "Eng")
        {
            text.text = textEng;
        }
    }
}
