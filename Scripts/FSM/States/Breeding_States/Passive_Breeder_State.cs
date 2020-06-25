using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive_Breeder_State : _State
{
    bool atHome = false;
    float time = 0;
    public override void EnterState(Peon peon)
    {
        // We can only reach this state if we have a partner and we are both breeders
        peon.stateString = "Passive breeder";
    }
    public override void Update(Peon peon)
    {
        //To get out from Passive Breeder state
        if (peon.partner.currentJob != Jobs.Breeder || peon.partner.stateString == "Passive breeder")
        {
            peon.TransitionToState(peon.transitionState);
        }

        if (peon.beenKissed && !atHome)
        {
            peon.MoveTo(peon.home.transform.position);
        }

        if (atHome && (peon.transform.position - peon.partner.transform.position).sqrMagnitude < 2f)
        {
            time += Time.deltaTime;
            if (time >= 5f)
            {
                ResetPeonBooleans(peon);

                if ((peon.gender == Gender.Male && peon.partner.gender == Gender.Male) || peon.gender == Gender.Female)
                {
                    peon.partner.TransitionToState(peon.partner.transitionState);
                    peon.TransitionToState(new Pregnant_State());
                }
                else if (peon.gender == Gender.Male)
                {
                    peon.partner.TransitionToState(new Pregnant_State());
                    peon.TransitionToState(peon.transitionState);
                }
            }
        }
    }

    private static void ResetPeonBooleans(Peon peon)
    {
        peon.canBeBreeder = false;
        peon.partner.canBeBreeder = false;

        peon.needNewJob = true;
        peon.partner.needNewJob = true;

        peon.beenKissed = false;
        peon.partner.beenKissed = false;
    }

    public override void OnTriggerEnter(Peon peon, Collider collider)
    {
        if (collider.gameObject == peon.home.gameObject)
        {
            atHome = true;
            peon.StopMoving();
        }
    }
}
