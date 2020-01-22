using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align
{
    public Kinematic myBoi;

    public SteeringOutput getSteering()
    {
        //Calculate myBoi to delegate to align
        Vector3 direction = myBoi.transform.position - selectedBoi.transform.position;
        if (direction.magnitude == 0)
            return null;

        base.myBoi = myBoi;
        float angle = Mathf.Atan2(direction.x, direction.z);
        angle *= Mathf.Rad2Deg;
        base.myBoi.transform.eulerAngles = new Vector3(0, angle, 0);
        return base.getSteering();
    }
}