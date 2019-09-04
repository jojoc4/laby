using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the global timer
/// </summary>
public class Timer : MonoBehaviour
{

    public Text text;                                   //text with current time
    public static Stopwatch timer = new Stopwatch();    

    // Start is called before the first frame update
    void Start()
    {
        startTimer();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = getTime();
        if(Time.timeScale == 0)
        {
            if (timer.IsRunning)
                timer.Stop();
        }
        else
        {
            if (!timer.IsRunning)
                timer.Start();
        }
    }

    public static void startTimer()
    {
        timer.Reset();
        timer.Start();
    }

    public static void stopTimer()
    {
        timer.Stop();
    }

    public static string getTime()
    {
        System.TimeSpan t = timer.Elapsed;
        return string.Format("{0:D2}:{1:D2}.{2:D3}",
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);
    }
}
