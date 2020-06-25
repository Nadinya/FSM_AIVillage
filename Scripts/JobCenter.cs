using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GatherPriority
{
    public ResourceType resource;
    public int minAmount;
    public int maxAmount;
}
[System.Serializable]
public class JobPriority
{
    public Jobs job;
    public int priority;
}
public class JobCenter : MonoBehaviour
{
    public GatherPriority[] resourcePrio;
    public JobPriority[] jobPriorities;
    public HousingEstate housingEstate;

    [Header("Town Lists")]
    public List<Peon> workers = new List<Peon>();
    public ResourceCentre inventory;
    public Peon courter = null;
    public void AddWorkers(Peon peon)
    {
        workers.Add(peon);
        peon.TransitionToState(peon.transitionState);
    }
    public void RemoveWorker(Peon peon)
    {
        if (workers.Contains(peon))
        {
            workers.Remove(peon);
        }
    }
    public bool FindAPartner(Peon peon)
    {
        if (courter == null)
        {
            courter = peon;
            return false;
        }
        else if (courter != null && peon == courter)
        {
            Debug.Log("Cannot date yourself");
            return false;
        }
        else
        {
            courter.partner = peon;
            peon.partner = courter;

            courter = null;
            return true;
        }
    }
    public Resource RequestNode(ResourceType resourceRequest)
    {
        List<Resource> resRequest = new List<Resource>();

        foreach (Resource res in inventory.resourceInRadius)
        {
            if (res.resource == resourceRequest)
            {
                resRequest.Add(res);
            }
        }

        Resource nearestNode = null;

        if (resRequest.Count > 0)
        {
            float nearestDist = Mathf.Infinity;
            foreach (Resource res in resRequest)
            {
                if (res.chargesLeft >= 1)
                {
                    float dist = Vector3.Distance(res.transform.position, transform.position);
                    if (dist < nearestDist)
                    {
                        nearestNode = res;
                        nearestDist = dist;
                    }
                }
            }
        }
        return nearestNode;
    }
    public Jobs GetJob(Peon peon)
    {
        Jobs job = Jobs.Worker;

        Shuffle(resourcePrio);

        // Looking for NEEDED resources first.
        for (int i = 0; i < resourcePrio.Length; i++)
        {
            if (inventory.GetTempAmountInStorage(resourcePrio[i].resource) < resourcePrio[i].minAmount * workers.Count)
            {
                peon.resourceNeeded = resourcePrio[i].resource;
                return Jobs.Worker;
            }
        }

        // If all resources are above minimum go fill till its at the proper safety range.
        for (int i = 0; i < resourcePrio.Length; i++)
        {
            if (inventory.GetTempAmountInStorage(resourcePrio[i].resource) < resourcePrio[i].maxAmount * workers.Count)
            {
                peon.resourceNeeded = resourcePrio[i].resource;
                return Jobs.Worker;
            }
        }

        // If all needs are met find a random job.
        int randomJob = Random.Range(0, 6);

        if (randomJob < 3)
        {
            peon.resourceNeeded = (ResourceType)randomJob;
            return Jobs.Worker;
        }

        else if (randomJob >= 3 && randomJob < 4)
        {
            if (peon.canBeBreeder)
            {
                if (peon.home != null)
                {
                    if (peon.partner != null)
                    {
                        return Jobs.Breeder;
                    }
                    else
                    {
                        FindAPartner(peon);
                        return Jobs.Idle;
                    }
                }
                else
                {
                    return Jobs.Idle;
                }
            }
        }

        else if (randomJob >= 4 && randomJob < 5)
        {
            job = peon.canWorship ? Jobs.Worshipper : Jobs.Idle;
        }

        else
        {
            if (inventory.GetItemAmountInStorage(ResourceType.Wood) > 250 && (housingEstate.maxInhabitants - housingEstate.currentInhabitants) < 2)
            {
                return Jobs.Builder;
            }
            else
            {
                return Jobs.Idle;
            }
        }

        return job;
    }
    void Shuffle(GatherPriority[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int random = Random.Range(0, i);

            GatherPriority temp = array[i];

            array[i] = array[random];
            array[random] = temp;
        }
    }


    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.black;
    //     Gizmos.DrawWireSphere(transform.position, searchRadius);
    // }
}
