using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement
    { get { return playerMovement; } }

    [SerializeField]
    GameObject trashPile;

    int score;
    public int GetScore

    { get { return score; } }

    private void Start()
    {
        LevelTimer.Instnace.PlayAgain += OnPlayAgain;
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }

    void OnPlayAgain()
    {
        score = 0;
    }

    public void InceremtnScore(int aScoreToAdd)
    {
        score += aScoreToAdd;
        float scale = Map(score, 0, 30, 0, 1);
        scale = Mathf.Clamp01(scale);
        trashPile.transform.localScale = new Vector3(scale, scale, scale);
    }

    float Map(float aV, float aStart1, float aStop1, float aStart2, float aStop2)
    {
        return ((aV - aStart1) / (aStop1 - aStart1)) * (aStop2 - aStart2) + aStart2;
    }
}
