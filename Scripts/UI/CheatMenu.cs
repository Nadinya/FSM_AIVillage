using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour
{
    public GameObject villageButtonPrefab;
    public Transform buttonParent;
    public GameObject debugMenu;
    public GameObject villageDebugMenu;
    public InputField amountInput;


    [Header("Temp / Test")]
    public HousingEstate[] villageCenters;
    public ResourceCentre vc;
    public HolySite hs;


    int amount;

    void Awake()
    {
        villageCenters = FindObjectsOfType<HousingEstate>();

        for (int i = 0; i < villageCenters.Length; i++)
        {
            Cheat_Button vButton = Instantiate(villageButtonPrefab, buttonParent).GetComponent<Cheat_Button>();
            vButton.GetComponentInChildren<Text>().text = villageCenters[i].transform.name;

            vButton.housingEstate = villageCenters[i];
            vButton.cheatMenu = this;
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.Z))
        {
            debugMenu.SetActive(true);
        }
    }

    public void ChangeMenu()
    {
        villageDebugMenu.SetActive(true);
        debugMenu.SetActive(true);

        villageDebugMenu.GetComponentInChildren<Text>().text =vc.name + " debug menu";
    }


    #region Button Functions
    public void AddFood()
    {
        AddResource(ResourceType.Food);
    }
    public void AddWood()
    {
        AddResource(ResourceType.Wood);
    }
    public void AddStone()
    {
        AddResource(ResourceType.Stone);
    }
    public void AddBelief()
    {
        if (amountInput.text != null)
        {
            amount = int.Parse(amountInput.text);
        }

        if (amount != 0)
        {
            hs.ModifyBelief(amount);
        }

    }
    #endregion

    private void AddResource(ResourceType resources)
    {
        if (amountInput.text != null)
        {
            amount = int.Parse(amountInput.text);
        }

        if (amount != 0)
        {
            vc.Modify(resources, amount);
            vc.ModifyTemp(resources, amount);
        }
        else
        {
            Debug.LogError("No amount to add has been entered.");
        }
    }
}
