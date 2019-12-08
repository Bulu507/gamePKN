using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour
{
    #region Enums
    public enum MovementType  //Type of Movement
    {
        MoveTowards,
        LerpTowards
    }
    #endregion //Enums

    #region Public Variables
    public MovementType Type = MovementType.MoveTowards; // Movement type used
    public MovementPath MyPath; // Reference to Movement Path Used
    public float Speed = 1; // Speed object is moving
    public float MaxDistanceToGoal = .1f; // How close does it have to be to the point to be considered at point
    [SerializeField]
    public int rollDice;
    public int movingTo;
    #endregion //Public Variables

    #region Private Variables
    private IEnumerator<Transform> pointInPath; //Used to reference points returned from MyPath.GetNextPathPoint
    private int myPosition;
    #endregion //Private Variables

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

        MyPath.MovingTo = 1;

        //Sets up a reference to an instance of the coroutine GetNextPathPoint
        pointInPath = MyPath.GetNextPathPoint();
        //Debug.Log(pointInPath.Current);
        //Get the next point in the path to move to (Gets the Default 1st value)
        pointInPath.MoveNext();
        //Debug.Log(pointInPath.Current);

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

        myPosition = MyPath.StartFrom - 1;
        if (movingTo == myPosition && rollDice != 0)
        {
            movingTo = myPosition + (rollDice);
            rollDice = 0;
        }

        Debug.Log("Cekcurrent path = " + MyPath.StartFrom);
        Debug.Log("MOVE = " + MyPath.MovementDirection);

        if (MyPath.StartFrom == 19 && MyPath.IsIdle == true)
        {
            //int t = 20;
            //Debug.Log("t ===== " + t);
            //if (t < 0)
            //{
                movingTo = myPosition - (3);
                MyPath.MovementDirection = -1;
                rollDice = 0;
                Debug.Log("Harusnya maju");
            
            //MyPath.MovementDirection = 1;
            //}
        }

        //Debug.Log("Idle == " + MyPath.IdleStatus);

        if(MyPath.IsIdle == true)
        {
            Debug.Log("MANDEGGGGGG");
        }

        MyPath.MovingTo = movingTo;
        //Validate there is a path with a point in it
        if (pointInPath == null || pointInPath.Current == null)
        {
            return; //Exit if no path is found
        }

        if (Type == MovementType.MoveTowards) //If you are using MoveTowards movement type
        {
            //Move to the next point in path using MoveTowards
            transform.position =
                Vector3.MoveTowards(transform.position,
                                    pointInPath.Current.position,
                                    Time.deltaTime * Speed);
        }
        else if (Type == MovementType.LerpTowards) //If you are using LerpTowards movement type
        {
            //Move towards the next point in path using Lerp
            transform.position = Vector3.Lerp(transform.position,
                                                pointInPath.Current.position,
                                                Time.deltaTime * Speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, pointInPath.Current.rotation, Time.deltaTime * Speed * 1000);

        }

        //Check to see if you are close enough to the next point to start moving to the following one
        //Using Pythagorean Theorem
        //per unity suaring a number is faster than the square root of a number
        //Using .sqrMagnitude 
        //var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        //if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal) //If you are close enough
        //{
        //    pointInPath.MoveNext(); //Get next point in MovementPath
        //}
        //The version below uses Vector3.Distance same as Vector3.Magnitude which includes (square root)

        var distanceSquared = Vector3.Distance(transform.position, pointInPath.Current.position);
        if (distanceSquared < MaxDistanceToGoal) //If you are close enough
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
