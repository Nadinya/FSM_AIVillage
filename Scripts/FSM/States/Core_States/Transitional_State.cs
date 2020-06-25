using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitional_State : _State
{
    public override void EnterState(Peon peon)
    {
        peon.stateString = "Transitioning";

        if (peon.GetComponent<Hunger>().currentHunger <= 50)
        {
            if (peon.housingEstate.wareHouse.GetItemAmountInStorage(ResourceType.Food) > 5)
            {
                peon.TransitionToState(new Eating_State());
            }
            else
            {
                peon.TransitionToState(peon.gatherState);
                UIInfo.instance.SetText("Your villagers are hungry!");

            }
        }
        else if (peon.itemInHand.amount > 0)
        {
            peon.TransitionToState(peon.returnState);
        }
        else
        {
            if(peon.needNewJob)
            {
                peon.currentJob = peon.housingEstate.jobCenter.GetJob(peon);
            }

            switch (peon.currentJob)
            {
                case Jobs.Worker:
                    peon.TransitionToState(peon.gatherState);
                    break;

                case Jobs.Breeder:
                    peon.TransitionToState(new Breeder_State());
                    break;

                case Jobs.Worshipper:
                    peon.TransitionToState(new Worshipper_State());
                    break;

                case Jobs.Builder:
                    peon.TransitionToState(new Builder_State());
                    break;

                case Jobs.Idle:
                    peon.TransitionToState(new Idle_State());
                    break;

                default:
                    break;
            }
        }
    }

    public override void Update(Peon peon)
    {

    }
    public override void OnTriggerEnter(Peon peon, Collider collider)
    {

    }


}
