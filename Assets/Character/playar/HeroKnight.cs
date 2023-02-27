using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
    [Header("Effects")]
    [SerializeField] GameObject m_RunStopDust;
    [SerializeField] GameObject m_JumpDust;
    [SerializeField] GameObject m_LandingDust;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private Sensor_HeroKnight   m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
    private AudioManager_Hero   m_audioManager;
    private AudioSource         m_audioSource;
    private bool                m_isWallSliding = false;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private bool                m_attack = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;



    // Используйте это для инициализации
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        m_audioSource = GetComponent<AudioSource>();
        m_audioManager = AudioManager_Hero.instance;
    }

    // Обновление вызывается один раз за кадр
    void Update ()
    {
        m_attack = false;
        // Увеличьте таймер, управляющий комбинацией атак
        m_timeSinceAttack += Time.deltaTime;

        // Увеличьте таймер, который проверяет продолжительность броска
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Отключите прокатку, если таймер увеличивает продолжительность
        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Проверьте, только что ли персонаж приземлился на землю
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Проверьте, начал ли персонаж только что падать
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //-- Ручка ввода и перемещения --
        float inputX = Input.GetAxis("Horizontal");

        // Измените направление спрайта в зависимости от направления ходьбы
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>();
            transform.localScale = new Vector3(1, 1, 1);
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>();
            transform.localScale = new Vector3(-1, 1, 1);
        }


        // Движение
        if (!m_rolling || !m_attack)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Установите скорость полета в аниматоре
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Обрабатывать анимации --
        //Настенный слайд
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding);

        //Смерть
        //if (Input.GetKeyDown("e") && !m_rolling)
        //{
        //    m_animator.SetBool("noBlood", m_noBlood);
        //    m_animator.SetTrigger("Death");
        //}

        //Ранить
        //else if (Input.GetKeyDown("q") && !m_rolling)
        //    m_animator.SetTrigger("Hurt");

        //Атака
        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            
            m_currentAttack++;

            // Вернитесь к одному после третьей атаки
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Сбросить комбинацию атак, если время с момента последней атаки слишком велико
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Вызовите одну из трех анимаций атаки "Attack1", "Attack2", "Attack3".
            m_animator.SetTrigger("Attack" + m_currentAttack);
            // Таймер сброса
            m_attack = true;
            m_timeSinceAttack = 0.0f;
        }

        // Блок
        //else if (Input.GetMouseButtonDown(1) && !m_rolling)
        //{
        //    m_animator.SetTrigger("Block");
        //    m_animator.SetBool("IdleBlock", true);
        //}

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // перекат
        //else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        //{
        //    m_rolling = true;
        //    m_animator.SetTrigger("Roll");
        //    m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        //}
            

        // прыжок
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        // бег
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Таймер сброса
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        // положение покоя
        else
        {
            // Предотвращает мерцающие переходы в режим ожидания
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }


    // Анимационные мероприятия
    // Вызывается в анимации слайдов.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Установите правильное положение появления стрелки
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Поверните стрелку в правильном направлении
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
    void SpawnDustEffect(GameObject dust, float dustXOffset = 0)
    {
        if (dust != null)
        {
            // Установить положение появления пыли
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * m_facingDirection, 0.0f, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Поверните пыль в правильном направлении X
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(m_facingDirection, 1, 1);
        }
    }
    void AE_runStop()
    {
        m_audioManager.PlaySound("RunStop");
        float dustXOffset = 0.6f;
        SpawnDustEffect(m_RunStopDust, dustXOffset);
    }
    void AE_Attack()
    {
        m_audioManager.PlaySound("Attack");
    }
    void AE_footstep()
    {
        m_audioManager.PlaySound("Footstep");
    }

    void AE_Jump()
    {
        m_audioManager.PlaySound("Jump");
        SpawnDustEffect(m_JumpDust);
    }

    void AE_Landing()
    {
        m_audioManager.PlaySound("Landing");
        SpawnDustEffect(m_LandingDust);
    }
}