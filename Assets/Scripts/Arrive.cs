
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive
{
    public Kinematic selectedBoi;
    public Kinematic myBoi;

    float maxAcceleration = 100f;
    float maxSpeed = 10f;

    float myBoiRadius = 1.5f;

    float slowRadius = 3f;

    float timeToTarget = 0.1f;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        Vector3 direction = myBoi.transform.position - selectedBoi.transform.position;
        float distance = direction.magnitude;

      
        float myBoiSpeed = 0f;
        if (distance > slowRadius)
        {
            myBoiSpeed = maxSpeed;
        }
        else 
        {
            myBoiSpeed = maxSpeed * (distance - myBoiRadius) / myBoiRadius;
        }

        Vector3 myBoiVelocity = direction;
        myBoiVelocity.Normalize();
        myBoiVelocity *= myBoiSpeed;

        result.linearVelocity= myBoiVelocity - selectedBoi.linearVelocity;
        result.linearVelocity/= timeToTarget;

        if (result.linearVelocity.magnitude > maxAcceleration)
        {
            result.linearVelocity.Normalize();
            result.linearVelocity*= maxAcceleration;
        }

        return result;
    }
}
