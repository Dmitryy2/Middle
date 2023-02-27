using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiDemon : MonoBehaviour
{

    [SerializeField] float m_speed = 3.0f;
    private Animator m_animator;
    private Rigidbody2D m_body2d;


    public Transform player;

    private AudioManager_Hero m_audioManager;
    private AudioSource m_audioSource;

    float moveX;
    float moveY;
    public float angryDis;
    public float attackDis;
    void Start() {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
        m_audioManager = AudioManager_Hero.instance;
    }
    void Update() {

        float disp = Vector2.Distance(transform.position, player.position);
       
        if (disp < angryDis)
        {
            Angry();
        }
        else {
            Stop();
        }

        if (disp < attackDis)
        {
            Stop();
        }
    }
    void Angry() {
        if (player.position.x < transform.position.x)
        {
            moveX = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.position.x > transform.position.x) {
            moveX = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }


        if (player.position.y < transform.position.y) {
            moveY = -1;
        }
        else if (player.position.y > transform.position.y) {
            moveY = 1;
        }

        m_animator.SetInteger("AnimState", 2);
        m_body2d.velocity = new Vector2(moveX * m_speed, moveY * m_speed);
    }
    void Stop() {
        m_body2d.velocity = new Vector2(0, 0);
        m_animator.SetInteger("AnimState", 1);
    }
    void AE_EAttack() {
        m_audioManager.PlaySound("EAttack");
    }
    void AE_FAttack()
    {
        m_audioManager.PlaySound("FAttack");
    }
}
