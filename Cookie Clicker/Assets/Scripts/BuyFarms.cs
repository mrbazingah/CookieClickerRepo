using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyFarms : MonoBehaviour
{
    [SerializeField] double cost;
    [SerializeField] double costIncrease;
    [SerializeField] int amountOfFarms;
    [Space]
    [SerializeField] GameObject farm;
    [Space]
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI amountOfFarmsText;

    double max = 1000000000;
    double min = 1000000;
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
    }

    void Update()
    {
        amountOfFarms = FindObjectsOfType<Farm>().Length;
        amountOfFarmsText.text = amountOfFarms.ToString();

        RoundCost();
    }

    void RoundCost()
    {
        if (cost < min && currentEndText > 1)
        {
            min /= 1000;
            max /= 1000;

            currentEndText--;

            Debug.Log("Minus");
        }
        else if (cost >= max)
        {
            min *= 1000;
            max *= 1000;

            currentEndText++;

            Debug.Log("Plus");
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

        if (clickerEndText > currentEndText)
        {
            BuyFarm();
        }
        else if (amount >= cost && clickerEndText == currentEndText)
        {
            BuyFarm();
        }
    }

    void BuyFarm()
    {
        clicker.BuyAnything(cost);
        cost *= costIncrease;
        costText.text = cost.ToString();

        Instantiate(farm);
    }
}
