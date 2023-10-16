using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyRewardsUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject panel;

    [Header("Timer Elements")]
    [SerializeField] private GameObject claimButton;
    [SerializeField] private GameObject timerContainer;
    [SerializeField] private TextMeshProUGUI timerText;


    private int seconds;
    public void OpenPanel()
    {
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);


    }

    internal void ResetTimer()
    {
        int timerSeconds = 60 * 60 * 24 - 1; //23:59:59 para el proximo reward

        InitializeTimer(timerSeconds);
    }

    private void UpdateTimer()
    {
        seconds--;
        

        UpdateTimerText();

        if(seconds <= 0)
        {
            StopTimer();
        }
    }

    private void StopTimer()
    {
        CancelInvoke(nameof(UpdateTimer));

        claimButton.SetActive(true);
        timerContainer.SetActive(false);
    }

    private void UpdateTimerText()
    {
        timerText.text = TimeSpan.FromSeconds(seconds).ToString();

    }

    internal void InitializeTimer(int seconds)
    {
        claimButton.SetActive(false);
        timerContainer.SetActive(true);

        this.seconds = seconds;

        UpdateTimerText();

        InvokeRepeating(nameof(UpdateTimer), 0, 1);
    }

    internal void AllRewardsClaimed()
    {
        claimButton.SetActive(false);
        timerContainer.SetActive(false);
    }
}
