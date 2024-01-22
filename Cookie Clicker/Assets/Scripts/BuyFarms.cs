using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyFarms : MonoBehaviour
{
    [SerializeField] float cost;
    [SerializeField] float costIncrease;
    [SerializeField] float amountOfFarms;
    [Space]
    [SerializeField] GameObject farm;
    [Space]
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI amountOfFarmsText;

    float max = 1000000000;
    float min = 1000000;
    string[] endTexts;
    int currentEndText;
    string costTextString;

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
        if (cost < 1000)
        {
            costTextString = cost.ToString();
        }
        else if (cost < min && cost >= 1000000)
        {
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
        else if (cost >= 1000 && cost < min)
        {
            costTextString = (cost / 1000).ToString("F") + "K";
        }
        else if (cost >= min & cost < max)
        {
            costTextString = (cost / min).ToString("F") + endTexts[currentEndText];
        }

        costText.text = costTextString;
    }

    public void OnButtonClick()
    {
        float amount = clicker.GetAmount();

        if (amount >= cost)
        {
            clicker.BuyAnything(cost);
            cost *= costIncrease;
            cost = Mathf.Floor(cost);
            costText.text = cost.ToString();

            Instantiate(farm);
        }
    }
}
