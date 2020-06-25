using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breeder_State : _State
{
    bool hasKissed = false;
    public override void EnterState(Peon peon)
    {
        if(peon.partner != null)
        {
            // If our partner is already a breeder
            if (peon.partner.currentJob == Jobs.Breeder)
            {
                peon.TransitionToState(new Passive_Breeder_State());
            }
            else
            {
                peon.stateString = "Active Breeder";
                hasKissed = false;
                peon.partner.needNewJob = false;

                if (peon.partner.currentJob != Jobs.Worshipper)
                {
                    peon.partner.currentJob = Jobs.Breeder;
                }
                else
                {
                    peon.TransitionToState(peon.transitionState);
                }
            }
        }
    }
    public override void Update(Peon peon)
    {
        if (peon.partner.currentJob != Jobs.Worshipper)
        {
            if (!hasKissed)
            {
                peon.MoveTo(peon.partner.transform.position);
                if ((peon.transform.position - peon.partner.transform.position).sqrMagnitude < 2f)
                {
                    peon.MoveTo(peon.home.transform.position);
                    peon.partner.beenKissed = true;
                    hasKissed = true;
                }
            }
        }
        else
        {
            peon.TransitionToState(peon.transitionState);
        }
        
    }
    public override void OnTriggerEnter(Peon peon, Collider collider)
    {
        if(collider.gameObject == peon.home.gameObject)
        {
            peon.StopMoving();
        }
    }
}
