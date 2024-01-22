using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] float amountEarned;

    float amountEarnedAtStart;

    Clicker clicker;

    void Awake()
    {
        clicker = FindObjectOfType<Clicker>();

        amountEarnedAtStart = amountEarned;
        StartCoroutine(ClickDelay());
    }

    IEnumerator ClickDelay()
    {
        float multipliers = clicker.GetAllMultipliers();

        amountEarned = amountEarnedAtStart;
        amountEarned *= multipliers;

        yield return new WaitForSeconds(timer);

        clicker.ProcessBuildings(amountEarned);
        StartCoroutine(ClickDelay());
    }
}
