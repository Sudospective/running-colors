using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    [Tooltip("TMP Text UI content for timer")]
    [SerializeField] TMP_Text timerText;

    bool timerActive;

    float currentTime;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentTime = 0;
        StartTimer();
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);

        timerText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
