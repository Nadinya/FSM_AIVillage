using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public ResourceType resource;
    public int amount;
}
public class ResourceCentre : MonoBehaviour
{
    public int searchRadius = 100;
    public List<Resource> resourceInRadius;
    public Item[] startingResources;
    private Dictionary<ResourceType, int> inventory = new Dictionary<ResourceType, int>();
    private Dictionary<ResourceType, int> tempInventory = new Dictionary<ResourceType, int>();

    [Header("Inventory UI Panels")]
    public Text UIText;
    public Text UITextTemp;

    private void Awake()
    {
        for (int i = 0; i < startingResources.Length; i++)
        {
            Modify(startingResources[i].resource, startingResources[i].amount);
            ModifyTemp(startingResources[i].resource, startingResources[i].amount);
        }
        GetNodesInRadius();
    }

    private void Start()
    {
        UpdateUI();
    }

    private void GetNodesInRadius()
    {
        resourceInRadius = new List<Resource>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach(Collider coll in colliders)
        {
            if(coll.GetComponent<Resource>())
            {
                coll.GetComponent<Resource>().resourceCentre = this;
                resourceInRadius.Add(coll.GetComponent<Resource>());
            }
        }
    }

    public void RemoveNode(Resource resource)
    {
        resourceInRadius.Remove(resource);
    }

    public int GetTempAmountInStorage(ResourceType resource)
    {
        if (tempInventory.ContainsKey(resource))
        {
            return inventory[resource];
        }
        else return 0;
    }

    public int GetItemAmountInStorage(ResourceType resource)
    {
        if (inventory.ContainsKey(resource))
        {
            return inventory[resource];
        }
        else return 0;
    }

    public void ModifyTemp(ResourceType key, int value)
    {
        if (tempInventory.ContainsKey(key))
        {
            tempInventory[key] += value;
        }
        else
        {
            tempInventory.Add(key, value);
        }
        // TODO remove method since UI will not be shown for temporary resources
        UpdateUI();
    }


    public void Modify(ResourceType key, int value)
    {
        if (inventory.ContainsKey(key))
        {
            inventory[key] += value;
        }
        else
        {
            inventory.Add(key, value);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        UIText.text = "";
        UITextTemp.text = "";
        foreach (KeyValuePair<ResourceType, int> item in inventory)
        {
            UIText.text += item.Key.ToString() + " " + item.Value.ToString() + "\n";
        }
        foreach (KeyValuePair<ResourceType, int> item in tempInventory)
        {
            UITextTemp.text += item.Key.ToString() + " " + item.Value.ToString() + "\n";
        }
    }

  private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }

}
