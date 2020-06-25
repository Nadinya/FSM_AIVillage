using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheat_Button : MonoBehaviour
{
    public CheatMenu cheatMenu;
    public HousingEstate housingEstate;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        cheatMenu.vc = housingEstate.GetComponentInChildren<ResourceCentre>();
        cheatMenu.hs = housingEstate.GetComponentInChildren<HolySite>();
        cheatMenu.ChangeMenu();
    }
}
