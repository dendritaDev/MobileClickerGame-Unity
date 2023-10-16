using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;


[RequireComponent(typeof(DailyRewardsUI))]
public class DailyRewardsManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private DailyRewardContainer[] dailyRewardContainers;
    private DailyRewardsUI dailyRewardsUI;

    [Header(" Data ")]
    [SerializeField] private DailyReward[] dailyRewardsData;
    private int rewardIndex;
    private const string rewardIndexKey = "RewardIndex";
    private const string lastclaimKey = "LastClaimDateTime";
    private DateTime lastClaimDateTime;

    private void Awake()
    {
        dailyRewardsUI = GetComponent<DailyRewardsUI>();
        LoadData();

        if (!CheckIfAllRewardsHasBeenClaimed())
            dailyRewardsUI.OpenPanel();
    }

    // Start is called before the first frame update
    void Start()
    {
        ConfigureRewards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConfigureRewards()
    {
        for (int i = 0; i < dailyRewardContainers.Length; i++)
        {
            Sprite icon = dailyRewardsData[i].icon;
            string amount;
            if (dailyRewardsData[i].rewardType == DailyRewardType.Carrots)
                amount = DoubleUtilities.ToScientificNotation(dailyRewardsData[i].amount);
            else
                amount = dailyRewardsData[i].amount.ToString();
            
            string day = "Day " + (i + 1);

            bool claimed = false;

            if (rewardIndex > i)
                claimed = true;

            dailyRewardContainers[i].Configure(icon, amount, day, claimed);
        }
    }

    public void ClaimButtonCallback()
    {
        DailyReward dailyReward = dailyRewardsData[rewardIndex];

        RewardPlayer(dailyReward);

        rewardIndex++;

        SaveData();

        UpdateRewardContainers();

        if(!CheckIfAllRewardsHasBeenClaimed())
            ResetTimer();



        dailyRewardsUI.ClosePanel();
    }

    private bool CheckIfAllRewardsHasBeenClaimed()
    {
        if (rewardIndex > 6)
        {
            dailyRewardsUI.AllRewardsClaimed();
            return true;

        }
        return false;
    }

    private void ResetTimer()
    {
        dailyRewardsUI.ResetTimer();
    }

    private void UpdateRewardContainers()
    {
        for (int i = 0; i < dailyRewardContainers.Length; i++)
        {
            if (rewardIndex > i)
                dailyRewardContainers[i].Claim();

        }
    }

    private void RewardPlayer(DailyReward dailyReward)
    {
        switch (dailyReward.rewardType)
        {
            case DailyRewardType.Carrots:
                RewardCarrots(dailyReward.amount);

                break;

            case DailyRewardType.Upgrade:
                RewardUpgrade(dailyReward.upgradeInxed ,dailyReward.amount);
                break;

            default:
                break;
        }
    }
    private void RewardCarrots(double amount)
    {
        CarrotManager.instance.AddCarrots(amount);
    }

    private void RewardUpgrade(int upgradeInxed, double amount)
    {
        ShopManager.instance.RewardUpgrade(upgradeInxed, (int)amount);
    }


    private void LoadData()
    {
        rewardIndex = PlayerPrefs.GetInt(rewardIndexKey);
        if (LoadLastClaimDateTime())
            CheckIfCanClaim();
    }

    private void CheckIfCanClaim()
    {
        TimeSpan timeSpan = DateTime.Now.Subtract(lastClaimDateTime);
        double elapsedHours = timeSpan.TotalHours;

        if(elapsedHours < 24)
        {
            int seconds= 60*6*24 - (int)timeSpan.TotalSeconds;

            dailyRewardsUI.InitializeTimer(seconds);
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(rewardIndexKey , rewardIndex);
        SaveLastDataTime();

    }

    private bool LoadLastClaimDateTime()
    {

        bool validDateTime = DateTime.TryParse(PlayerPrefs.GetString(lastclaimKey), out lastClaimDateTime);


        return validDateTime;
    }

    private void SaveLastDataTime()
    {
        DateTime now = DateTime.Now;

        PlayerPrefs.SetString(lastclaimKey, now.ToString());
    }
}
