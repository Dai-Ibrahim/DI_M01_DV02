using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align
{
    public Kinematic selectedBoi;
    public Kinematic myBoi;

    float maxAngularAcceleration = 100f; // 5
    float maxRotation = 45f; // maxAngularVelocity

    //Set radius where myBoi starts to slow down
    float slowRadius = 10f;

    float timeToTarget = 0.1f;//Get up to speed time

   
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

        float angularAcceleration = Mathf.Abs(result.angularVelocity);
        if (angularAcceleration > maxAngularAcceleration) //is acceleration going over the max
        {
            result.angularVelocity/= angularAcceleration;
            result.angularVelocity*= maxAngularAcceleration;
        }

        result.linearVelocity = Vector3.zero;
        return result;
    }
}
