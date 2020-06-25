using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public HousingEstate housingEstate;
    public int maxInhabitants = 4;
    public List<Peon> inhabitants;

    private void Awake()
    {
        inhabitants = new List<Peon>();

    }

    void Start()
    {
        housingEstate = GetComponentInParent<HousingEstate>();

        housingEstate.AddHome(this);
    }
    public bool MoveIn(Peon peon)
    {
        if (inhabitants.Count < maxInhabitants)
        {
            inhabitants.Add(peon);
            housingEstate.currentInhabitants++;
            return true;
        }
        return false;
    }

    public void MoveOut(Peon peon)
    {
        inhabitants.Remove(peon);
        housingEstate.currentInhabitants--;
    }
}
