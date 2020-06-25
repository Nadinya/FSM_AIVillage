using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worshipper_State : _State
{
    public bool atHolyPlace = false;
    float time = 0;
    public override void EnterState(Peon peon)
    {
        peon.stateString = "Worshipper";
        peon.canWorship = false;
        
        if ((peon.transform.position - peon.housingEstate.holySite.transform.position).sqrMagnitude > 2)
        {
            peon.MoveTo(peon.housingEstate.holySite.transform.position);
        }
        else
        {
            peon.needNewJob = true;
            peon.TransitionToState(peon.transitionState);
        }
    }

    public override void Update(Peon peon)
    {
        if (atHolyPlace)
        {
            time += Time.deltaTime;
            if (time > 11)
            {
                peon.housingEstate.holySite.ModifyBelief(10);
                peon.needNewJob = true;
                peon.TransitionToState(peon.transitionState);
            }
        }
    }
    public override void OnTriggerEnter(Peon peon, Collider collider)
    {
        if (collider.gameObject == peon.housingEstate.holySite.gameObject)
        {
            peon.StopMoving();
            peon.TriggerAnimation("isPraying");
            atHolyPlace = true;
        }
    }
}
