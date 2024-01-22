using TMPro;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [SerializeField] float cost;
    [SerializeField] float costIncrease;
    [SerializeField] float multipler;
    [Space]
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI multiplerText;

    float max = 1000000000;
    float min = 1000000;
    string[] endTexts;
    int currentEndText;
    string costTextString;

    Clicker clicker;

    void Awake()
    {
        clicker = FindObjectOfType<Clicker>();
        endTexts = clicker.GetEndTexts();

        costText.text = cost.ToString();
        multiplerText.text = multipler.ToString() + "X";
    }

    void Update()
    {
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
            clicker.AddMultiplier(cost, multipler);
            cost *= costIncrease;
            cost = Mathf.Floor(cost);
            costText.text = cost.ToString();
        }
    }
}
