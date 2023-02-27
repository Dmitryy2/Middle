using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using UnityEngine;
public class EndGame : MonoBehaviour
{

    private Animator m_animator;
    public GameObject fireworks;
    public GameObject EndText;
    public GameObject EndText1;
    public Text       Text1;
    public GameObject Text2;
    public GameObject Battle;
    public GameObject GameOver;
    public GameObject PrincessHart;
    public GameObject Enemy;
    private AudioManager_Hero m_audioManager;
    private AudioSource m_audioSource;

    public string language;
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioManager = AudioManager_Hero.instance;
    }
    void Update()
    {
        language = PlayerPrefs.GetString("language");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        int maxCoin = 303;
        int minCoin = 250;
        int NoCoin = minCoin - Coin.coin;
        if (Coin.coin >= 250)
        {
            fireworks.SetActive(true);
            EndText.SetActive(true);
            m_audioManager.PlaySound("End");
            Battle.SetActive(false);
            Text2.SetActive(false);
            PrincessHart.SetActive(true);
            Destroy(Enemy);
            endGame();
        }
        else if (Coin.coin >= 0 && Coin.coin <= 249)
        {
            EndText1.SetActive(true);

            if (language == "" || language == "Ru")
            {
                Text1.text = "Вам не хватает " + NoCoin + " монет для закрытия ипотеки";
            }
            else if (language == "Eng")
            {
                Text1.text = "You are missing " + NoCoin + " coins to close the mortgage";
            }
            PrincessHart.SetActive(true);
        }
    }
    private async Task endGame()
    {
        await Task.Delay(TimeSpan.FromSeconds(10f));
        Application.LoadLevel("_Main");
    }
}