using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axPendulim : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private EdgeCollider2D EdgeCollider2D;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    //References
    private Animator m_animator;
    private HpSystem playerHealth;

    [SerializeField] public float rSpeed = 0;



    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        transform.Rotate(0.0f, 0.0f, rSpeed * Time.deltaTime);
        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;

                DamagePlayer();
            }
        }
    }


    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(EdgeCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(EdgeCollider2D.bounds.size.x * range, EdgeCollider2D.bounds.size.y, EdgeCollider2D.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<HpSystem>();


        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(EdgeCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(EdgeCollider2D.bounds.size.x * range, EdgeCollider2D.bounds.size.y, EdgeCollider2D.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}