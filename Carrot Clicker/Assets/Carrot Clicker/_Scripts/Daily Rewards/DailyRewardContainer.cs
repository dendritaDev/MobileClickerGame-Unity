using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private GameObject claimElements;
    public void Configure(Sprite icon, string amount, string day, bool claimed)
    {
        iconImage.sprite = icon;
        amountText.text = amount;
        dayText.text = day;


        if (claimed)
            Claim();
        
    }

    public void Claim()
    {
        claimElements.SetActive(true);
    }
}
