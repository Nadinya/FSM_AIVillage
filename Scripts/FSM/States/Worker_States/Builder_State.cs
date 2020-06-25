using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder_State : _State
{
    Vector3 destination;
    int town = 1 << LayerMask.NameToLayer("Building");
    int resource = 1 << LayerMask.NameToLayer("Resource");
    int mask;

    public override void EnterState(Peon peon)
    {
        peon.stateString = "Builder";

        mask = town | resource;

        peon.housingEstate.wareHouse.ModifyTemp(ResourceType.Wood, -250);

        FindRandomLocation(peon);
    }

    public override void Update(Peon peon)
    {
        base.Update(peon);

        if ((peon.transform.position - destination).sqrMagnitude < 2)
        {
            peon.StopMoving();

            Collider[] colls = Physics.OverlapSphere(peon.transform.position, 25);
            foreach (Collider coll in colls)
            {
                if (coll.gameObject.layer == mask)
                {
                    FindRandomLocation(peon);
                    break;
                }
            }

            if (peon.housingEstate.workshop == null)
            {
                peon.housingEstate.wareHouse.Modify(ResourceType.Wood, -250);

                Workshop workshop = GameObject.Instantiate(peon.housingEstate.workshopPrefab);

                peon.housingEstate.workshop = workshop;
                workshop.transform.name = "Workshop";
                workshop.transform.position = destination;
                peon.TransitionToState(peon.transitionState);
            }
            else
            {
                peon.housingEstate.wareHouse.Modify(ResourceType.Wood, -250);

                int rHouse = Random.Range(0, peon.housingEstate.workshop.homePrefabs.Length);
                Home homePrefab = peon.housingEstate.workshop.homePrefabs[rHouse];

                // Do NOT 'simplify'. Code will not work if you do.
                Home home = GameObject.Instantiate(homePrefab, peon.housingEstate.transform);

                home.transform.position = destination;
                peon.TransitionToState(peon.transitionState);
            }



        }
    }
    public override void OnTriggerEnter(Peon peon, Collider collider)
    {

    }

    private void FindRandomLocation(Peon peon)
    {
        float x = peon.housingEstate.wareHouse.transform.position.x + Random.Range(-30, 30);
        float z = peon.housingEstate.wareHouse.transform.position.z + Random.Range(-30, 30);
        destination = new Vector3(x, peon.transform.position.y, z);
        peon.MoveTo(destination);
    }
}

