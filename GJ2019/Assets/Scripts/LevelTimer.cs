using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    private static LevelTimer instance;
    public static LevelTimer Instnace
    { get { return instance; } }

    [SerializeField]
    float startTime = 60.0f;
    float currentTime;

    bool tickDown = true;

    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField]
    GameObject scoreBoard;
    [SerializeField]
    TextMeshProUGUI[] scorePlaceText;

    [SerializeField]
    GameObject playAgainButton;

    [SerializeField]
    private CanvasGroup m_gameOverCG;

    public delegate void OnTimerEnd();
    public OnTimerEnd TimerEnd;

    public delegate void OnPlayAgain();
    public OnTimerEnd PlayAgain;

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


        m_gameOverCG.alpha = 0;

        OnTimerStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (tickDown)
        {
            timerText.text = "Time Left: " + currentTime.ToString("F2");
            currentTime -= 1.0f * Time.deltaTime;

            if (currentTime < 0.0f)
            {
                tickDown = false;
                TimerEnd();
            }
        }
    }

    public void OnTimerStart()
    {
        currentTime = startTime;
        tickDown = true;
    }

    public void OnTimerOver()
    {
        currentTime = 0f;
        timerText.text = "Time Left: " + currentTime.ToString("F2");

        PlayerScore[] playerScores = FindObjectsOfType<PlayerScore>();

        List<int> scores = new List<int>();
        for (int i = 0; i < playerScores.Length; i++)
        {
            scores.Add(playerScores[i].GetScore);
        }

        //sort scores
        scores.Sort();
        scores.Reverse();

        for (int i = 0; i < scorePlaceText.Length; i++)
        {
            string text = "";
            switch (i)
            {
                case 0:
                    text = "1st Place: " + playerScores[i].PlayerMovement.Colour() + " " + scores[i];
                    break;
                case 1:
                    text = "2nd Place: " + playerScores[i].PlayerMovement.Colour() + " " + scores[i];
                    break;
                case 2:
                    text = "3rd Place: " + playerScores[i].PlayerMovement.Colour() + " " + scores[i];
                    break;
                case 3:
                    text = "4th Place: " + playerScores[i].PlayerMovement.Colour() + " " + scores[i];
                    break;
            }

            scorePlaceText[i].text = text;
        }

        StartCoroutine(ShowGameOver());
        
    }

    private IEnumerator ShowGameOver()
    {
        float step = 0f;

        do
        {
            step += Time.deltaTime / 2f;//Over 2 seconds
            m_gameOverCG.alpha = step;
            yield return null;
        }
        while (m_gameOverCG.alpha < 1f);

        m_gameOverCG.interactable = true;

        Time.timeScale = 0.0f;

        //scoreBoard.SetActive(true);
        //playAgainButton.SetActive(true);
        playAgainButton.GetComponent<Button>().Select();

    }

    public void PlayAgainButton()
    {
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        m_gameOverCG.interactable = false;
        Time.timeScale = 1.0f;

        float step = 1f;
        do
        {
            step -= Time.deltaTime / 1f;
            m_gameOverCG.alpha = step;
            yield return null;
        }
        while (m_gameOverCG.alpha > 0f);

        //scoreBoard.SetActive(false);
        //playAgainButton.SetActive(false);

        PlayAgain();

        OnTimerStart();

    }
}
