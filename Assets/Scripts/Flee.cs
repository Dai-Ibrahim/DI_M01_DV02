using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee
{
    public Kinematic myBoi; 
    public Kinematic selectedBoi; 

    public float maxAcceleration = 15f;


    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
        result.linearVelocity = selectedBoi.transform.position - myBoi.transform.position;

        result.linearVelocity.Normalize();
        result.linearVelocity *= maxAcceleration;

        result.angularVelocity = 0;
        return result;
    }
}
