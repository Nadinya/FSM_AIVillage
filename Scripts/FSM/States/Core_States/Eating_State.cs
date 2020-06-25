using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating_State : _State
{
    float time;
    Hunger hunger;
    public override void EnterState(Peon peon)
    {
        peon.stateString = "Eating";

        peon.housingEstate.wareHouse.ModifyTemp(ResourceType.Food, -10);
        peon.TriggerAnimation("isSitting");
        hunger = peon.GetComponent<Hunger>();
        hunger.isEating = true;
        
    }
    public override void Update(Peon peon)
    {
        time += Time.deltaTime;
        if(time >= 17 )
        {
            hunger.Eat();
            time = 0;
            hunger.isEating = false;
            hunger = null;
            peon.housingEstate.wareHouse.Modify(ResourceType.Food, -10);
            peon.TransitionToState(peon.transitionState);
        }
    } 
    public override void OnTriggerEnter(Peon peon, Collider collider)
    {

    }
}
