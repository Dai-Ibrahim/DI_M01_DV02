using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align
{
    public Kinematic selectedBoi;
    public Kinematic myBoi;

    float maxAngularAcceleration = 100f; // 5
    float maxRotation = 45f; // maxAngularVelocity

    // the radius for arriving at the myBoi
    // the radius for beginning to slow down
    float slowRadius = 10f;

    // the time over which to achieve myBoi speed
    float timeToTarget = 0.1f;

    // returns the angle in degrees that we want to align with
    // Align will rotate to match the myBoi's oriention
    // sub-classes can overwrite this function to set a different myBoi angle e.g. to face a myBoi
    public virtual float getMyBoiAngle()
    {
        return myBoi.transform.eulerAngles.y;
    }

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // get the naive direction to the myBoi
        //float rotation = Mathf.DeltaAngle(selectedBoi.transform.eulerAngles.y, myBoi.transform.eulerAngles.y);
        float rotation = Mathf.DeltaAngle(selectedBoi.transform.eulerAngles.y, getMyBoiAngle());
        float rotationSize = Mathf.Abs(rotation);


        // if we are outside the slow radius, then use maximum rotation
        float myBoiRotation = 0.0f;
        if (rotationSize > slowRadius)
        {
            myBoiRotation = maxRotation;
        }
        else // otherwise use a scaled rotation
        {
            myBoiRotation = maxRotation * rotationSize / slowRadius;
        }

        // the final myBoiRotation combines speed (already in the variable) and direction
        myBoiRotation *= rotation / rotationSize;

        // acceleration tries to get to the myBoi rotation
        // something is breaking my angularVelocty... check if NaN and use 0 if so
        float currentAngularVelocity = float.IsNaN(selectedBoi.angularVelocity) ? 0f : selectedBoi.angularVelocity;
        result.angularVelocity= myBoiRotation - currentAngularVelocity;
        result.angularVelocity/= timeToTarget;

        // check if the acceleration is too great
        float angularAcceleration = Mathf.Abs(result.angularVelocity);
        if (angularAcceleration > maxAngularAcceleration)
        {
            result.angularVelocity/= angularAcceleration;
            result.angularVelocity*= maxAngularAcceleration;
        }

        result.linearVelocity = Vector3.zero;
        return result;
    }
}