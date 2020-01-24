using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align
{
    public Kinematic selectedBoi;
    public Kinematic myBoi;

    float maxAngularAcceleration = 100f; // 5
    float maxRotation = 45f; 

    float slowRadius = 10f;

    float timeToTarget = 0.1f;

   
    public virtual float getMyBoiAngle()
    {
        return myBoi.transform.eulerAngles.y;
    }

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

       
        float rotation = Mathf.DeltaAngle(selectedBoi.transform.eulerAngles.y, getMyBoiAngle());
        float rotationSize = Mathf.Abs(rotation);


        float myBoiRotation = 0.0f;
        if (rotationSize > slowRadius)
        {
            myBoiRotation = maxRotation;
        }
        else 
        {
            myBoiRotation = maxRotation * rotationSize / slowRadius;
        }

        
        myBoiRotation *= rotation / rotationSize;

      
        float currentAngularVelocity = float.IsNaN(selectedBoi.angularVelocity) ? 0f : selectedBoi.angularVelocity;
        result.angularVelocity= myBoiRotation - currentAngularVelocity;
        result.angularVelocity/= timeToTarget;

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
