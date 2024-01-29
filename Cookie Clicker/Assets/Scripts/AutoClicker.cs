using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClicker : MonoBehaviour
{
    [SerializeField] float timeBeforeClick;

    Clicker clicker;

    void Awake()
    {
        clicker = FindObjectOfType<Clicker>();

        StartCoroutine(ClickDelay());
    }

    IEnumerator ClickDelay()
    {
        yield return new WaitForSeconds(timeBeforeClick);

        clicker.OnButtonClick();
        StartCoroutine(ClickDelay());
    }
}
