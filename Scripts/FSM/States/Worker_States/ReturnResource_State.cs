using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnResource_State : _State
{
    public bool atCenter = false;
    float time = 0;

    GameObject location;
    public override void EnterState(Peon peon)
    {
        peon.stateString = "Returning Resource";

        atCenter = false;
        time = 0;

        if (peon.itemInHand.resource == ResourceType.Food)
        {
            location = peon.housingEstate.foodStorage.gameObject;
        }
        else
        {
            location = peon.housingEstate.nonFoodStorage.gameObject;
        }

        peon.MoveTo(location.transform.position);
    }
    public override void Update(Peon peon)
    {
        if (atCenter)
        {
            time += Time.deltaTime;
            if (time >= 2.333f)
            {
                if (peon.itemInHand.amount > 0)
                {
                    peon.housingEstate.wareHouse.GetComponent<ResourceCentre>().Modify(peon.itemInHand.resource, peon.itemInHand.amount);
                    peon.itemInHand.amount = 0;
                }
                peon.TransitionToState(peon.transitionState);
            }
        }

        base.Update(peon);
    }

    public override void OnTriggerEnter(Peon peon, Collider collider)
    {
        if (collider.gameObject == location)
        {
            peon.StopMoving();
            peon.TriggerAnimation("isDropping");
            atCenter = true;
        }
    }


}
