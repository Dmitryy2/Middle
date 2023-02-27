using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpavnFier : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    float RandX;
    float RandY;
    Vector2 whereToSpawn;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float lx = 150;
    [SerializeField] private float rx = 390;
    [SerializeField] private float dy = 9;
    [SerializeField] private float uy = 10;
    float nextSpawn = 0.0f;


    private AudioManager_Hero m_audioManager;
    private AudioSource m_audioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioManager = AudioManager_Hero.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            RandX = Random.Range(lx, rx);
            RandY = Random.Range(dy, uy);

            whereToSpawn = new Vector2(RandX, RandY);
            Instantiate(obj, whereToSpawn, Quaternion.identity);
        }
    }
}