using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _State
{
    public abstract void EnterState(Peon peon);
    public virtual void Update(Peon peon)
    {
        if (peon.beenKissed)
        {
            peon.TransitionToState(new Breeder_State());
        }

    }

    public abstract void OnTriggerEnter(Peon peon, Collider collider);

  
}
