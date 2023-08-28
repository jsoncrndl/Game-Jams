using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Animator anim;
    private TimeSpan timer;

    public TextMeshProUGUI timerText;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        timer += TimeSpan.FromSeconds(Time.deltaTime);
        timerText.text = timer.ToString(@"m\:ss\:ff");
    }

    public void StartTimer()
    {
        Time.timeScale = 1;
        timer = TimeSpan.Zero;
    }

    public void StopTimer()
    {
        Time.timeScale = 0;
    }
}