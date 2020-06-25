using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayDuration = 20f;
    public float nightDuration = 10f;

    public bool isDayTime = true;

    public delegate void NewDay();
    public static event NewDay newDay;
    private void Start()
    {
        StartCoroutine(TimeCycle());
    }

    IEnumerator TimeCycle()
    {
        while (true)
        {
            isDayTime = true;
            yield return new WaitForSeconds(dayDuration);

            isDayTime = false;
            yield return new WaitForSeconds(nightDuration);
            newDay();
        }
    }

}
