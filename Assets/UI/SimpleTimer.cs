using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
public class SimpleTimer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Text timerText;

    private float _timeLeft = 0f;
    private bool _timerOn = false;


    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameOver;

    private void Start()
    {
        _timeLeft = time;
        _timerOn = true;
    }

    private void Update()
    {
        if (_timerOn)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                UpdateTimeText();
            }
            else
            {
                _timeLeft = time;
                _timerOn = false;
                GameOver();
            }
        }
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
    private async Task GameOver()
    {
        Time.timeScale = 0;
        await Task.Delay(TimeSpan.FromSeconds(2f));
        Application.LoadLevel("GameOver2");
    }

}