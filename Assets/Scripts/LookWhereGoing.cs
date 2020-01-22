using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWhereGoing : Align
{
    public SteeringOutput getSteering()
    {
        //1. Calculate target to delegate to align
        Vector3 velocity = selectedBoi.linearVelocity;
        if (velocity.magnitude == 0)
            return null;

        //Otherwise set target based on velocity
        float angle = Mathf.Atan2(velocity.x, velocity.z);
        angle *= Mathf.Rad2Deg;
        myBoi.transform.eulerAngles = new Vector3(0, angle, 0);
        return base.getSteering();

    }
}