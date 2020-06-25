using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    public float currentHunger;
    public float maxHunger = 100;
    public float hungerDecline = 1;
    public bool isEating = false;

    private bool isSlowed = false;
    private void Awake()
    {
        currentHunger = maxHunger;
    }

    public void Eat()
    {
        currentHunger = maxHunger;
        if (isSlowed)
        {
            isSlowed = false;
            Debug.Log("Speed up villager");
        }
    }

    private void Update()
    {
        if (!isEating)
        {
            currentHunger -= hungerDecline * Time.deltaTime;
            if (currentHunger <= (maxHunger / 4) && !isSlowed)
            {
                Debug.Log("Slowing " + this.name);
                isSlowed = true;
            }
            if (currentHunger <= 0)
            {
                //Debug.Log("Dead " + this.name);
                currentHunger = 0;
            }
        }

    }
}
