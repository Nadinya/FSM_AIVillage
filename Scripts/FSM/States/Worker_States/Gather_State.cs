using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather_State : _State
{
    bool hasGathered = false;
    Resource target;

    float time;
    float gatherTime;
    string triggerString;

    public override void EnterState(Peon peon)
    {
        peon.stateString = "Gather";

        ResetParameters();
        SetDestination(peon);
    }
    public override void Update(Peon peon)
    {
        if(hasGathered)
        {
            time += Time.deltaTime;
            if (time >= gatherTime)
            {
                target.Gather(peon);
                target = null;

                peon.TransitionToState(peon.returnState);                
            }
        }
        base.Update(peon);
    }

    public override void OnTriggerEnter(Peon peon, Collider collider)
    {
        if(collider.gameObject == target.gameObject)
        {
            peon.StopMoving();
            peon.TriggerAnimation(triggerString);
            hasGathered = true;
        }
    }

    private void SetDestination(Peon peon)
    {
        target = peon.housingEstate.jobCenter.RequestNode(peon.resourceNeeded);

        if(target != null)
        {
            peon.housingEstate.wareHouse.ModifyTemp(target.resource, target.amountToGive);
            // save parameters
            gatherTime = target.gatherTime;
            triggerString = target.animTrigger;

            // set new amount of times resource can be gathered
            target.TakeResource();

            // set destinations
            Vector3 destination = target.transform.position;
            peon.MoveTo(destination);
        }
        else
        {
            Debug.Log("No Target");
        }
        
    }

    private void ResetParameters()
    {
        hasGathered = false;
        time = 0;
        gatherTime = 0;
        triggerString = string.Empty;
    }
}
