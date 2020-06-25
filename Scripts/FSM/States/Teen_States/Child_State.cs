using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child_State : _State
{
    Vector3 destination;
    int rDance;

    bool atLocation = false;
    float time = 0;
    public override void EnterState(Peon peon)
    {
        peon.stateString = "Child";
        FindRandomLocation(peon);
    }

    public override void Update(Peon peon)
    {
        if(!atLocation)
        {
            if ((peon.transform.position - destination).sqrMagnitude < 3)
            {
                Play(peon);
                atLocation = true;
            }
        }
        if(atLocation)
        {
            time += Time.deltaTime;
            if(time > 10f)
            {
                FindRandomLocation(peon);
                atLocation = false;
                time = 0;
            }
        }
    }

    private void Play(Peon peon)
    {
        peon.StopMoving();
        rDance = Random.Range(1, 3);
        peon.TriggerAnimation("dance" + rDance);

    }

    public override void OnTriggerEnter(Peon peon, Collider collider)
    {
    
    }
    private void FindRandomLocation(Peon peon)
    {
        float x = peon.housingEstate.wareHouse.transform.position.x + Random.Range(-30, 30);
        float z = peon.housingEstate.wareHouse.transform.position.z + Random.Range(-30, 30);
        destination = new Vector3(x, peon.transform.position.y, z);
        peon.MoveTo(destination);
    }
}
