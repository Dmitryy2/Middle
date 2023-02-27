using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSystem : MonoBehaviour
{

    [SerializeField] private Animator m_animator;

    public int maxHealth = 100;
    public int currentHealth;
    private AudioManager_Hero m_audioManager;
    private AudioSource m_audioSource;
    float time;
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        m_audioManager = AudioManager_Hero.instance;
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage) {

        currentHealth -= damage;
        m_animator.SetTrigger("Hurt");
        m_audioManager.PlaySound("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Update()
    {
    }
    public void setHealth(int bonusHealth) {
        currentHealth += bonusHealth;

        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }
    void Die() {
        time = 0f;
        
        m_animator.SetTrigger("Death");
        m_animator.SetTrigger("Death");
        m_animator.SetTrigger("Death");
        m_animator.SetTrigger("Death");
        m_audioManager.PlaySound("Death");
        this.enabled = false;
        
    }
}