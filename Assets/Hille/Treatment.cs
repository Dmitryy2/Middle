using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treatment : MonoBehaviour
{
    public int Heal = 15;
    private string collisionTag = "Player";
    private AudioManager_Hero m_audioManager;
    private AudioSource m_audioSource;
    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioManager = AudioManager_Hero.instance;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == collisionTag) {
            HpSystem health = collision.gameObject.GetComponent<HpSystem>();
            health.setHealth(Heal);
            m_audioManager.PlaySound("Hille");
            Destroy(gameObject);
        }
    }
}
