using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : Singleton<HudManager>
{
    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private TextMeshProUGUI timerText;

    public void ChangePoints(int points)
    {
        pointsText.text = $"POINTS: {points}";
    }

    public void ChangeTimer(int time)
    {
        timerText.text = $"TIMER: {time}";
    }
}