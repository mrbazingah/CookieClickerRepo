using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Clicker : MonoBehaviour
{
    [SerializeField] double amountPerClick;
    [SerializeField] double amount;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] string[] endTexts;

    double max = 1000000000;
    double min = 1000000;
    string amountTextString;
    int currentEndText;

    bool isOver1000;

    int allMultipliers = 1;

    void Update()
    {
        RoundAmount();

        if (currentEndText > endTexts.Length - 1)
        {
            currentEndText = 0;
        }
    }

    void RoundAmount()
    {
        if (amount < min && currentEndText > 1)
        {
            min /= 1000;
            max /= 1000;

            currentEndText--;

            Debug.Log("Minus");
        }
        else if (amount >= max)
        {
            min *= 1000;
            max *= 1000;

            currentEndText++;

            Debug.Log("Plus");
        }
        else if (amount >= 1000 && !isOver1000)
        {
            currentEndText++;

            isOver1000 = true;
        }
        else if (currentEndText == 1 && amount >= min)
        {
            currentEndText++;
        }
        else if (amount >= min && amount < max)
        {
            amountTextString = (amount / min).ToString("F") + endTexts[currentEndText];
        }
        else if (currentEndText < 1)
        {
             amountTextString = amount.ToString() + endTexts[currentEndText];
        }
        else if (currentEndText == 1)
        {
            amountTextString = (amount / 1000).ToString("F") + endTexts[currentEndText];
        }

        amountText.text = amountTextString;
    }

    public void OnButtonClick()
    {
        amount += amountPerClick;
    }

    public void AddMultiplier(double cost, int multiplier)
    {
        amount -= cost;
        amountPerClick *= multiplier;
        allMultipliers *= multiplier;
    }

    public void ProcessBuildings(double amountEarned)
    {
        amount += amountEarned;
    }

    public void AddToAmountPerClick(double amountAdded)
    {
        amountPerClick += amountAdded;
    }

    public void BuyAnything(double cost)
    {
        amount -= cost;
    }

    public double GetAmount()
    {
        return amount;
    }

    public double GetAmountPerClick()
    {
        return amountPerClick;
    }

    public int GetAllMultipliers()
    {
        return allMultipliers;
    }

    public string[] GetEndTexts()
    {
        return endTexts;
    }

    public int GetCurrentEndText()
    {
        return currentEndText;
    }
}
