using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageCounter : MonoBehaviour
{
    public Text tempText;
    public GameObject villagers;
    public HousingEstate he;
    
    void Update()
    {
        int v = villagers.transform.childCount;
        int m = he.maxInhabitants;
        tempText.text = v + " / " + m;
    }
}
