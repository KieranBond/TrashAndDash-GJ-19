using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    private static LevelTimer instance;
    public static LevelTimer Instnace
    { get { return instance; } }

    [SerializeField]
    float startTime = 60.0f;
    float currentTime;

    [SerializeField]
    TextMeshPro timerText;

    public delegate void OnTimerEnd();
    public OnTimerEnd TimerEnd;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Time Left: " + currentTime;
        currentTime -= 1.0f * Time.deltaTime;

        if(currentTime <= 0.0f)
        {
            TimerEnd();
        }
    }
}
