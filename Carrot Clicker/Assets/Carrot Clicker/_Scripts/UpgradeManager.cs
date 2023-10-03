using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AddCarrots), 1, 1);
    }

    private void AddCarrots()
    {
        UpgradeSO[] upgrades = ShopManager.instance.GetUpgrades();

        if (upgrades.Length > 1)
            return;

        double totalCarrots = 0;

        for (int i = 0; i < upgrades.Length; i++)
        {
            double upgradeCarrots = upgrades[i].cpsPerLevel * ShopManager.instance.GetUpgradeLevel(i);
            totalCarrots += upgradeCarrots;
        }

        CarrotManager.instance.AddCarrots(totalCarrots);
    }


}
