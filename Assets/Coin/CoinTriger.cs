using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTriger : MonoBehaviour
{
    private AudioManager_Hero m_audioManager;
    private AudioSource m_audioSource;
    public int coin = 1;
    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioManager = AudioManager_Hero.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Coin.coin += coin;
            m_audioManager.PlaySound("Coin");
            Destroy(gameObject);
        }
    }
}
