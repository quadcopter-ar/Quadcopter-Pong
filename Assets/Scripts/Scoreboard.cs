using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{

    public TextMeshPro[] scoreboardTexts;
    public int playerOneScore, playerTwoScore;
    public bool scored;

    // Start is called before the first frame update
    void Start()
    {
        playerOneScore = 0;
        playerTwoScore = 0;
        for (int i=0; i<scoreboardTexts.Length; i++)
        {
            scoreboardTexts[i].SetText($"{playerOneScore} - {playerTwoScore}");
        }
        scored = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (scored)
        {
            for (int i = 0; i < scoreboardTexts.Length; i++)
            {
                scoreboardTexts[i].SetText($"{playerOneScore} - {playerTwoScore}");
            }
            scored = false;
        }
    }
}
