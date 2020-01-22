using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek
{
    public Kinematic myBoi; //target
    public Kinematic selectedBoi; //char

    public float maxAcceleration = 15f;


    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
        result.linearVelocity = myBoi.transform.position - selectedBoi.transform.position;

        // give full acceleration along this direction
        result.linearVelocity.Normalize();
        result.linearVelocity *= maxAcceleration;

        result.angularVelocity = 0;
        return result;
    }
}