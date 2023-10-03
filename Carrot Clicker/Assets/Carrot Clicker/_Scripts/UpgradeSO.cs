using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Data", menuName = "Scriptable Objects/Upgrade Data", order = 0)]
public class UpgradeSO : ScriptableObject
{
    //https://blog.kongregate.com/the-math-of-idle-games-part-i/

    [Header(" General ")]
    public Sprite icon;
    public string title;

    [Header(" Settings")]
    public double cpsPerLevel;
    public double basePrice;
    public float coefficient;

    public double GetPrice(int level)
    {
        return basePrice * Mathf.Pow(coefficient, level);
    }


}
