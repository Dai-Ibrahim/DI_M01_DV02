using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum steeringBehaviors {
    Seek,Flee,Arrive,Align,Face,LookWhereGoing, None
    }

public class Kinematic : MonoBehaviour
{

    public Vector3 linearVelocity;
    public float angularVelocity; //In degrees
    public Kinematic myBoi;

    public steeringBehaviors choiceOfBehavior;

    // Update is called once per frame
    void Update()
    {
        switch (choiceOfBehavior)
        {
            case steeringBehaviors.None:
                ResetOrientation();
                break;
            default:
                MainSteeringBehaviors();
                break;
        }
    }


    void MainSteeringBehaviors()
    {
        ResetOrientation();

        switch (choiceOfBehavior)
        {
            case steeringBehaviors.Seek:
                Seek seek = new Seek();
                seek.selectedBoi = this;
                seek.myBoi = myBoi;
                SteeringOutput seeking = seek.getSteering();
                if (seeking != null)
                {
                    linearVelocity += seeking.linearVelocity* Time.deltaTime;
                    angularVelocity += seeking.angularVelocity * Time.deltaTime;
                }
                break;
            case steeringBehaviors.Flee:
                Flee flee = new Flee();
                flee.selectedBoi = this;
                flee.myBoi = myBoi;
                SteeringOutput fleeing = flee.getSteering();
                if (fleeing != null)
                {
                    linearVelocity += fleeing.linearVelocity* Time.deltaTime;
                    angularVelocity += fleeing.angularVelocity * Time.deltaTime;
                }
                break;
    
            case steeringBehaviors.Align:
                Align align = new Align();
                align.selectedBoi = this;
                align.myBoi = myBoi;
                SteeringOutput aligning = align.getSteering();
                if (aligning != null)
                {
                    linearVelocity += aligning.linearVelocity* Time.deltaTime;
                    angularVelocity += aligning.angularVelocity * Time.deltaTime;
                }
                break;
            case steeringBehaviors.Face:
                Face face = new Face();
                face.selectedBoi = this;
                face.myBoi = myBoi;
                SteeringOutput facing = face.getSteering();
                if (facing != null)
                {
                    linearVelocity += facing.linearVelocity* Time.deltaTime;
                    angularVelocity += facing.angularVelocity * Time.deltaTime;
                }
                break;
            case steeringBehaviors.LookWhereGoing:
                LookWhereGoing look = new LookWhereGoing();
                look.selectedBoi = this;
                look.myBoi = myBoi;
                SteeringOutput looking = look.getSteering();
                if (looking != null)
                {
                    linearVelocity += looking.linearVelocity* Time.deltaTime;
                    angularVelocity += looking.angularVelocity * Time.deltaTime;
                }
                break;
			case steeringBehaviors.Arrive:
                Arrive arrive = new Arrive();
                arrive.selectedBoi = this;
                arrive.myBoi = myBoi;
                SteeringOutput arriving = arrive.getSteering();
                if (arriving != null)
                {
                    linearVelocity += arriving.linearVelocity* Time.deltaTime;
                    angularVelocity += arriving.angularVelocity * Time.deltaTime;
                }
                break;
        }

    }

     void ResetOrientation()
    {
        //Update position and rotation
        transform.position += linearVelocity * Time.deltaTime;
        Vector3 angularVelocityIncrement = new Vector3(0, angularVelocity * Time.deltaTime, 0);
        transform.eulerAngles += angularVelocityIncrement;
    }

    public void SetSeeking()
    {
        choiceOfBehavior= steeringBehaviors.Seek;
    }
    public void SetFleeing()
    {
        choiceOfBehavior= steeringBehaviors.Flee;
    }
    public void SetArriving()
    {
        choiceOfBehavior= steeringBehaviors.Arrive;
    }
  
    public void SetFacing()
    {
        choiceOfBehavior= steeringBehaviors.Face;
    }
    public void SetLooking()
    {
        choiceOfBehavior= steeringBehaviors.LookWhereGoing;
    }
	public void SetAligning()
    {
        choiceOfBehavior= steeringBehaviors.Align;
    }
}