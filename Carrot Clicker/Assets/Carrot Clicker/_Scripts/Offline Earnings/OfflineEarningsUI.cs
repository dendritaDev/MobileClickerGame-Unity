using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineEarningsUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private OfflineEarningsPopup popup;

    public void DisplayPopup(double earnings)
    {
        popup.Configure(earnings.ToString("F2"));

        popup.GetClaimButton().onClick.AddListener(() => ClaimButtonClickedCallback(earnings));

        popup.gameObject.SetActive(true);
    }

    private void ClaimButtonClickedCallback(double earnings)
    {
        popup.gameObject.SetActive(false);

        CarrotManager.instance.AddCarrots(earnings);
    }
}
