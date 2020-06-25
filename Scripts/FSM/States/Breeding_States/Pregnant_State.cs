using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pregnant_State : _State
{
    Vector3 destination;
    float time = 0;
    bool hasTriggered = false;
    public override void EnterState(Peon peon)
    {
        peon.stateString = "Pregnant";

        float x = peon.transform.position.x + Random.Range(-15, 15);
        float z = peon.transform.position.z + Random.Range(-15, 15);
        destination = new Vector3(x, peon.transform.position.y, z);

        peon.MoveTo(destination);
    }
    public override void Update(Peon peon)
    {
        if(!hasTriggered && (peon.transform.position - destination).sqrMagnitude <= 2f)
        {
            hasTriggered = true;
            peon.StopMoving();
            peon.TriggerAnimation("isSitting");
            
        }
        if(hasTriggered)
        {
            time += Time.deltaTime;
            if (time > 17f)
            {
                peon.Birth();
                peon.TransitionToState(peon.transitionState);
            }
        }
    }
    public override void OnTriggerEnter(Peon peon, Collider collider)
    {
    }
}
