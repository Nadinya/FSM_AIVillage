using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Resource> resourceInVillage = new List<Resource>();

    public void AddResource(Resource resource)
    {
        resourceInVillage.Add(resource);
    }
    public void RemoveResource(Resource resource)
    {
        resourceInVillage.Remove(resource);
    }
    
}
