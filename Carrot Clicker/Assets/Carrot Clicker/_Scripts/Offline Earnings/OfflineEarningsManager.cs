using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OfflineEarningsUI))]
public class OfflineEarningsManager : MonoBehaviour
{
    [Header(" Elements ")]
    private OfflineEarningsUI offlineEarningsUI;

    [Header("Settings ")]
    [SerializeField] private int maxOfflineSeconds = 86400; //segundos que hay en un día entero
    private DateTime lastDateTime;
    private void Start()
    {
        offlineEarningsUI = GetComponent<OfflineEarningsUI>();

        if (LoadLastDateTime())
            CalculateOfflineSeconds();
        else
            Debug.Log("Unable to parse the last date time");
    }

    private void CalculateOfflineSeconds()
    {
        TimeSpan timeSpan = DateTime.Now.Subtract(lastDateTime);
        
        int offlineSeconds = (int)timeSpan.TotalSeconds;

        offlineSeconds = Mathf.Min(offlineSeconds, maxOfflineSeconds);

        CalculateOfflineEarnings(offlineSeconds);

    }

    private void CalculateOfflineEarnings(int offlineSeconds)
    {
        if(UpgradeManager.instance == null)
        {
            LeanTween.delayedCall(Time.deltaTime, () => CalculateOfflineEarnings(offlineSeconds));
        }

        double offlineEarnings = offlineSeconds * UpgradeManager.instance.GetCarrotsPerSecond();

        if (offlineEarnings < 0)
            return;

        offlineEarningsUI.DisplayPopup(offlineEarnings);
    }

    private void OnApplicationFocus(bool focus) //false =Cuando la app pierda el focus: La cerremos, minimicemos, etc.
    {
        if (focus) 
            return;

        SaveCurentDataTime();

    }

    private bool LoadLastDateTime()
    {
        
        bool validDateTime = DateTime.TryParse(PlayerPrefs.GetString("LastDateTime"), out lastDateTime);


        return validDateTime;
    }

    private void SaveCurentDataTime()
    {
        DateTime now = DateTime.Now;

        PlayerPrefs.SetString("LastDateTime", now.ToString());
    }
}
