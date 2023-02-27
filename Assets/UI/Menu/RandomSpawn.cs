using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    float RandX;
    float RandY;
    Vector2 whereToSpawn;
    [SerializeField]
    private float spawnRate = 2f;
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
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            RandX = Random.Range(412f, 425f);
            RandY = Random.Range(9.0f, 10.0f);

            whereToSpawn = new Vector2(RandX, RandY);
            Instantiate(obj, whereToSpawn, Quaternion.identity); 
            
            m_audioManager.PlaySound("Fierwork");
        }
    }
}