using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject player;
    public GameObject Enemy;
    float playerHealth;
    void Start()
    {
        Destroy(Enemy);
        player.GetComponent<HeroKnight>().enabled = false;
        EndDisplar();
    }
    private async Task EndDisplar() {
        await Task.Delay(TimeSpan.FromSeconds(2f));
        Application.LoadLevel("GameOver1");
    }
}
