using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    TextMeshPro scoreText;

    int score;
    public int GetScore
    { get { return score; } }

    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void InceremtnScore(int aScoreToAdd)
    {
        score += aScoreToAdd;
    }
}
