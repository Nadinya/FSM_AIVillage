using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_State : _State
{
    Vector3 destination;

    public override void EnterState(Peon peon)
    {
        peon.stateString = "Idle";

        float x = peon.transform.position.x + Random.Range(-15, 15);
        float z = peon.transform.position.z + Random.Range(-15, 15);
        destination = new Vector3(x, peon.transform.position.y, z);

        peon.MoveTo(destination);
    }
    public override void Update(Peon peon)
    {
        base.Update(peon);

        if((peon.transform.position - destination).sqrMagnitude < 3f)
        {
            peon.TransitionToState(peon.transitionState);
        }
    }

    public override void OnTriggerEnter(Peon peon, Collider collider)
    {

    }
}
