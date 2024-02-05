using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuyAutoClickers : MonoBehaviour
{

    [SerializeField] double cost;
    [SerializeField] double costIncrease;
    [SerializeField] int amountOfAutoClickers;
    [Space]
    [SerializeField] GameObject autoClicker;
    [Space]
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI amountOfAutoClickerText;

    double max = 1000000000;
    double min = 1000000;
    double minAtStart;

    string[] endTexts;
    int currentEndText;
    string costTextString;

    bool isOver1000;

    Clicker clicker;

    void Awake()
    {
        clicker = FindObjectOfType<Clicker>();

        costText.text = cost.ToString();
        endTexts = clicker.GetEndTexts();

        minAtStart = min;
    }

    void Update()
    {
        amountOfAutoClickers = FindObjectsOfType<AutoClicker>().Length;
        amountOfAutoClickerText.text = amountOfAutoClickers.ToString();

        RoundCost();
    }

    void RoundCost()
    {
        if (cost < min && currentEndText > 1)
        {
            if (cost < minAtStart)
            {
                currentEndText = 1;
                return;
            }

            min /= 1000;
            max /= 1000;

            currentEndText--;
        }
        else if (cost >= max)
        {
            min *= 1000;
            max *= 1000;

            currentEndText++;
        }
        else if (cost >= 1000 && !isOver1000)
        {
            currentEndText++;

            isOver1000 = true;
        }
        else if (currentEndText == 1 && cost >= min)
        {
            currentEndText++;
        }
        else if (cost >= min && cost < max)
        {
            costTextString = (cost / min).ToString("F") + endTexts[currentEndText];
        }
        else if (currentEndText < 1)
        {
            costTextString = cost.ToString() + endTexts[currentEndText];
        }
        else if (currentEndText == 1)
        {
            costTextString = (cost / 1000).ToString("F") + endTexts[currentEndText];
        }

        costText.text = costTextString;
    }

    public void OnButtonClick()
    {
        double amount = clicker.GetAmount();
        int clickerEndText = clicker.GetCurrentEndText();

        if ((clickerEndText > currentEndText) || (amount >= cost && clickerEndText == currentEndText))
        {
            BuyAutoClicker();
        }
    }

    void BuyAutoClicker()
    {
        clicker.BuyAnything(cost);
        cost *= costIncrease;
        costText.text = cost.ToString();

        Instantiate(autoClicker);
    }
}
