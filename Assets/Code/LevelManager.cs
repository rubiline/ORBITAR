using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string levelName;
    public TimeSpan goldTime;
    public TimeSpan silverTime;
    public TimeSpan bronzeTime;
    public int totalGems;
    public TimeSpan currentTime;
    public Stopwatch timer;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic("DISTANT_NEBULA");
    }

    // Update is called once per frame
    void Update()
    {
        //currentTime = timer.Elapsed;
    }

    private void StartTimer()
    {
        timer = new Stopwatch();
        timer.Start();
    }
}
