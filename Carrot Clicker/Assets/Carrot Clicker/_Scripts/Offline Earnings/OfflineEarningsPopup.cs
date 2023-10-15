using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OfflineEarningsPopup : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI earningsText;
    [SerializeField] private Button claimButton;
    
    public void Configure(string earningsString)
    {
        earningsText.text =earningsString;
    }

    public Button GetClaimButton()
    {
        return claimButton;
    }
}
