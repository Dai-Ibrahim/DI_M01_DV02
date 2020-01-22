
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive
{
    public Kinematic selectedBoi;
    public Kinematic myBoi;

    float maxAcceleration = 100f;
    float maxSpeed = 10f;

    // the radius for arriving at the myBoi
    float myBoiRadius = 1.5f;

    // the radius for beginning to slow down
    float slowRadius = 3f;

    // the time over which to achieve myBoi speed
    float timeToTarget = 0.1f;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // get the direction to the myBoi
        Vector3 direction = myBoi.transform.position - selectedBoi.transform.position;
        float distance = direction.magnitude;

        //if (distance < myBoiRadius)
        //{
        //    return null;
        //}

        // if we are outside the slow radius, then move at max speed
        float myBoiSpeed = 0f;
        if (distance > slowRadius)
        {
            myBoiSpeed = maxSpeed;
        }
        else // otherwise calculate a scaled speed
        {
            //myBoiSpeed = -(maxSpeed * distance / slowRadius); // should slowRadius here instead be myBoiRadius?
            myBoiSpeed = maxSpeed * (distance - myBoiRadius) / myBoiRadius;
        }

        // the myBoi velocity combines speed and direction
        Vector3 myBoiVelocity = direction;
        myBoiVelocity.Normalize();
        myBoiVelocity *= myBoiSpeed;

        // acceleration tries to get to the myBoi velocity
        result.linearVelocity= myBoiVelocity - selectedBoi.linearVelocity;
        result.linearVelocity/= timeToTarget;

        // check if the acceleration is too fast
        if (result.linearVelocity.magnitude > maxAcceleration)
        {
            result.linearVelocity.Normalize();
            result.linearVelocity*= maxAcceleration;
        }

        return result;
    }
}