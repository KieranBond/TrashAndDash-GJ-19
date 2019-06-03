using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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
    TextMeshProUGUI timerText;

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

        TimerEnd += OnTimerOver;

        OnTimerStart();
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

    public void OnTimerStart()
    {
        currentTime = startTime;
    }

    public void OnTimerOver()
    {
        PlayerScore[] playerScores = FindObjectsOfType<PlayerScore>();

        List<int> scores = new List<int>();
        for (int i = 0; i < playerScores.Length; i++)
        {
            scores.Add(playerScores[i].GetScore);
        }

        //sort scores
        scores.Sort();
        scores.Reverse();

        foreach (var item in scores)
        {
            Debug.Log(item);
        }

        OnTimerStart();
    }
}
