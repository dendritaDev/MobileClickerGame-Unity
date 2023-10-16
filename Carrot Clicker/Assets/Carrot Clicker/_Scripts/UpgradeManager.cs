using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [Header("Settings")]
    [Tooltip("Value in hertz")]
    [SerializeField] private int addCarrotsFrequency;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

    }
        // Start is called before the first frame update
        void Start()
    {
        InvokeRepeating(nameof(AddCarrots), 1, 1f / addCarrotsFrequency); //En vez de llamarlo cada segundo, lo llamamos mas pero tambien disminuimos el valor en addcarrots que suma por este mismo, para compensar
    }

    private void AddCarrots()
    {
        Debug.Log(GetCarrotsPerSecond());

        double totalCarrots = GetCarrotsPerSecond();
        CarrotManager.instance.AddCarrots(totalCarrots / addCarrotsFrequency);
    }

    public double GetCarrotsPerSecond()
    {

        UpgradeSO[] upgrades = ShopManager.instance.GetUpgrades();

        if (upgrades.Length <= 1)
            return 0;

        double totalCarrots = 0;

        for (int i = 0; i < upgrades.Length; i++)
        {
            double upgradeCarrots = upgrades[i].cpsPerLevel * ShopManager.instance.GetUpgradeLevel(i);
            totalCarrots += upgradeCarrots;
        }

        return totalCarrots;
    }

}
