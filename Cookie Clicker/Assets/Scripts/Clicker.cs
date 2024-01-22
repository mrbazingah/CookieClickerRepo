using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Clicker : MonoBehaviour
{
    [SerializeField] float amountPerClick;
    [SerializeField] float amount;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] string[] endTexts;

    float max = 1000000000;
    float min = 1000000;
    string amountTextString;
    int currentEndText;

    float allMultipliers = 1;

    void Update()
    {
        RoundAmount();
    }

    public void RoundAmount()
    {
        if (amount < 1000)
        {
            amountTextString = amount.ToString();
        }
        else if (amount < min && amount >= 1000000)
        {
            min /= 1000;
            max /= 1000;
            currentEndText--;
        }
        else if (amount >= max)
        {
            min *= 1000;
            max *= 1000;
            currentEndText++;
        }
        else if (amount >= 1000 && amount < min)
        {
            amountTextString = (amount / 1000).ToString("F") + "K";
        }
        else if (amount >= min & amount < max)
        {
            amountTextString = (amount / min).ToString("F") + endTexts[currentEndText];
        }

        amountText.text = amountTextString;
    }

    public void OnButtonClick()
    {
        amount += amountPerClick;
    }

    public void AddMultiplier(float cost, float multiplier)
    {
        amount -= cost;
        amountPerClick *= multiplier;
        allMultipliers *= multiplier;
    }

    public void ProcessBuildings(float amountEarned)
    {
        amount += amountEarned;
    }

    public void AddToAmountPerClick(float amountAdded)
    {
        amountPerClick += amountAdded;
    }

    public void BuyAnything(float cost)
    {
        amount -= cost;
    }

    public float GetAmount()
    {
        return amount;
    }

    public float GetAmountPerClick()
    {
        return amountPerClick;
    }

    public float GetAllMultipliers()
    {
        return allMultipliers;
    }

    public string[] GetEndTexts()
    {
        return endTexts;
    }
}
