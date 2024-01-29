using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField] int timer;
    [SerializeField] double amountEarned;

    double amountEarnedAtStart;

    Clicker clicker;

    void Awake()
    {
        clicker = FindObjectOfType<Clicker>();

        timer /= timer;
        amountEarned /= timer;

        amountEarnedAtStart = amountEarned;
        StartCoroutine(ClickDelay());
    }

    IEnumerator ClickDelay()
    {
        int multipliers = clicker.GetAllMultipliers();

        amountEarned = amountEarnedAtStart;
        amountEarned *= multipliers;

        yield return new WaitForSeconds(timer);

        clicker.ProcessBuildings(amountEarned);
        StartCoroutine(ClickDelay());
    }
}
