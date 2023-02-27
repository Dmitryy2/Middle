using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattack : MonoBehaviour
{

    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private PolygonCollider2D PolygonCollider2D;

    [Header("Player Layer")]
    [SerializeField] private LayerMask EnemyLayer;
    private float cooldownTimer = Mathf.Infinity;

    private HpSystem playerHealth;

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= 0.5)
            {
                cooldownTimer = 0;

                DamagePlayer();
            }
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(PolygonCollider2D.bounds.center + transform.right * 1 * transform.localScale.x * colliderDistance,
            new Vector3(PolygonCollider2D.bounds.size.x * 1, PolygonCollider2D.bounds.size.y, PolygonCollider2D.bounds.size.z),
            0, Vector2.left, 0, EnemyLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<HpSystem>();


        return hit.collider != null;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(PolygonCollider2D.bounds.center + transform.right * 1 * transform.localScale.x * colliderDistance,
            new Vector3(PolygonCollider2D.bounds.size.x * 1, PolygonCollider2D.bounds.size.y, PolygonCollider2D.bounds.size.z));
    }
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
