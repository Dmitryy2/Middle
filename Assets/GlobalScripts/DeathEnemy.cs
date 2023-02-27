using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEnemy : MonoBehaviour
{
    [SerializeField]
    public int heal;
    public Collider2D player1;
    [SerializeField] private GameObject obj;
    Vector2 whereToSpawn;
    void Start()
    {
        Coin.coin+=5;
        HpSystem treatment = player1.gameObject.GetComponent<HpSystem>();
        treatment.setHealth(heal);
        whereToSpawn = new Vector2(transform.position.x, transform.position.y);
        Instantiate(obj, whereToSpawn, Quaternion.identity);
        GetComponent<EnemyAi>().enabled = false;
        GetComponent<TriggerAttack>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().Sleep();
        Destroy(gameObject);
    }
}
