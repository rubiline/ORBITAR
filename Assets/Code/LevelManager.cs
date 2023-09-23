using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string levelName;
    public string music;
    public TimeSpan goldTime;
    public TimeSpan silverTime;
    public TimeSpan bronzeTime;
    public int totalGems;
    public int collectedGems;
    public TimeSpan currentTime;
    public Stopwatch timer;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        AudioManager.Instance.PlayMusic(music);
        StartTimer();
        currentTime = new TimeSpan(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer != null)
        {
            currentTime = timer.Elapsed;
        }
        timerText.text = currentTime.ToString(@"m\:ss\.fff");
    }

    private void OnApplicationQuit()
    {
        timer.Stop();
    }

    private void StartTimer()
    {
        timer = new Stopwatch();
        timer.Start();
    }
}
