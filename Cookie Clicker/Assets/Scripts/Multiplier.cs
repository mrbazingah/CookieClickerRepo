using TMPro;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [SerializeField] double cost;
    [SerializeField] double costIncrease;
    [SerializeField] int multipler;
    [Space]
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI multiplerText;

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
        endTexts = clicker.GetEndTexts();

        costText.text = cost.ToString();
        multiplerText.text = multipler.ToString() + "X";

        minAtStart = min;
    }

    void Update()
    {
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
        
        if (amount >= cost)
        {
            clicker.AddMultiplier(cost, multipler);
            cost *= costIncrease;
            costText.text = cost.ToString();
        }
    }
}
