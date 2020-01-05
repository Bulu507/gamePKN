using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour
{
    #region Enums

    #endregion //Enums

    #region Public Variables
    public MovementPath MyPath; // Reference to Movement Path Used
    public int diceValue; // value of rolldice
    #endregion //Public Variables

    #region Private Variables
    private float Speed = 4; // Speed object is moving
    private float MaxDistanceToGoal = .1f; // How close does it have to be to the point to be considered at point
    private IEnumerator<Transform> pointInPath; //Used to reference points returned from MyPath.GetNextPathPoint
    private int movingValue = 0; // used to container dicevalue + current position
    #endregion //Private Variables

    #region Setter Getter

    #endregion //Setter Getter

    // (Unity Named Methods)
    #region Main Methods
    public void Start()
    {
        //Make sure there is a path assigned
        if (MyPath == null)
        {
            Debug.LogError("Movement Path cannot be null, I must have a path to follow.", gameObject);
            return;
        }

        //Sets up a reference to an instance of the coroutine GetNextPathPoint
        pointInPath = MyPath.GetNextPathPoint();
        //Debug.Log(pointInPath.Current);
        //Get the next point in the path to move to (Gets the Default 1st value)
        pointInPath.MoveNext();
        //Debug.Log(pointInPath.Current);

        //set move to forward
        MyPath.Move = "forward";

        //Make sure there is a point to move to
        if (pointInPath.Current == null)
        {
            Debug.LogError("A path must have points in it to follow", gameObject);
            return; //Exit Start() if there is no point to move to
        }

        //Set the position of this object to the position of our starting point
        transform.position = pointInPath.Current.position;
    }

    //Update is called by Unity every frame
    public void Update()
    {
        //Validate there is a path with a point in it
        if (pointInPath == null || pointInPath.Current == null)
        {
            return; //Exit if no path is found
        }

        // when idle and turn to rolldice
        if (MyPath.IsIdle == true)
        {
            MyPath.GoingTo = movingValue + diceValue;
            movingValue = MyPath.GoingTo;
            diceValue = 0;
        }

        ////when on a bonus
        //if (MyPath.IsIdle == true && movingValue == 4)
        //{
        //    MyPath.GoingTo = movingValue + 3;
        //    movingValue = MyPath.GoingTo;
        //    diceValue = 0;
        //}

        //when on a zonk
        if (MyPath.IsIdle == true && MyPath.MovingTo == 8)
        {
            MyPath.Move = "backward";
            MyPath.GoingTo = movingValue - 3;
            movingValue = MyPath.GoingTo;
            diceValue = 0;
        }


        //Move towards the next point in path using Lerp
        transform.position = Vector3.Lerp(transform.position,
                                            pointInPath.Current.position,
                                            Time.deltaTime * Speed);

        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal) //If you are close enough
        {
            pointInPath.MoveNext(); //Get next point in MovementPath
        }
    }
    #endregion //Main Methods

    //(Custom Named Methods)
    #region Utility Methods 

    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines

    #endregion //Coroutines
}
