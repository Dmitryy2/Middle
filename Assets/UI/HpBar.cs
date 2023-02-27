using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpBar : MonoBehaviour
{
    public Image bar;
    public Text textbar;
    private float fill;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        fill = 1f;
    }

    // Update is called once per frame
    void Update()
    {

        float playerHealth = player.GetComponent<HpSystem>().currentHealth;
        float p = 100.0f;    
        fill = playerHealth / p;
        bar.fillAmount = fill;
        textbar.text = playerHealth.ToString() + "/100";
    }
}
