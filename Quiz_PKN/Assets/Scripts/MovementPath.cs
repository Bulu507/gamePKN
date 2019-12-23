using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MovementPath : MonoBehaviour
{
    #region Enums
    #endregion //Enums

    #region Public Variables
    public int movementDirection = 1; //1 clockwise/forward || -1 counter clockwise/backwards
    public int movingTo = 0; //used to identify point in PathSequence we are moving to
    public Transform[] PathSequence; //Array of all points in the path
    public int goingTo; //variable param movingTo + dice
    #endregion //Public Variables

    #region Private Variables
    private String move;
    private Boolean isIdle;
    #endregion //Private Variables

    #region Setter Getter
    public int MovingTo
    {
        get { return movingTo; }
        set { movingTo = value; }
    }
    public int GoingTo
    {
        get { return goingTo; }
        set { goingTo = value; }
    }
    public String Move
    {
        get { return move; }
        set { move = value; }
    }
    public Boolean IsIdle
    {
        get { return isIdle; }
        set { isIdle = value; }
    }
    #endregion

    // (Unity Named Methods)
    #region Main Methods
    //Update is called by Unity every frame
    void Update()
    {

    }

    //OnDrawGizmos will draw lines between our points in the Unity Editor
    //These lines will allow us to easily see the path that
    //our moving object will follow in the game
    public void OnDrawGizmos()
    {
        //Make sure that your sequence has points in it
        //and that there are at least two points to constitute a path
        if (PathSequence == null || PathSequence.Length < 2)
        {
            return; //Exits OnDrawGizmos if no line is needed
        }

        //Loop through all of the points in the sequence of points
        for (var i = 1; i < PathSequence.Length; i++)
        {
            //Draw a line between the points
            Gizmos.DrawLine(PathSequence[i - 1].position, PathSequence[i].position);
        }

    }
    #endregion //Main Methods

    //(Custom Named Methods)
    #region Utility Methods 

    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines
    //GetNextPathPoint() returns the transform component of the next point in our path
    //FollowPath.cs script will inturn move the object it is on to that point in the game
    public IEnumerator<Transform> GetNextPathPoint()
    {
        //Make sure that your sequence has points in it
        //and that there are at least two points to constitute a path
        if (PathSequence == null || PathSequence.Length < 1)
        {
            yield break; //Exits the Coroutine sequence length check fails
        }

        while (true) //Does not infinite loop due to yield return!!
        {
            //Return the current point in PathSequence
            //and wait for next call of enumerator (Prevents infinite loop)
            yield return PathSequence[movingTo];
            //*********************************PAUSES HERE******************************************************//
            //If there is only one point exit the coroutine
            if (PathSequence.Length == 1)
            {
                continue;
            }

            //If Linear path move from start to end then end to start then repeat

            ////If you are at the begining of the path
            //if (movingTo <= 0)
            //{
            //    movementDirection = 1; //Seting to 1 moves forward
            //}
            ////Else if you are at the end of your path
            //else if (movingTo >= PathSequence.Length - 1)
            //{
            //    movementDirection = -1; //Seting to -1 moves backwards
            //}
            if (move == "forward")
            {
                if (goingTo == 0)
                {
                    isIdle = true;
                    continue;
                }
                else if (movingTo <= goingTo - 1)
                {
                    isIdle = false;
                    movementDirection = 1; //Seting to 1 moves forward
                }
                //Else if you are at the end of your path
                //else if (movingTo >= PathSequence.Length - 1)
                else if (movingTo >= goingTo - 1)
                {
                    //movementDirection = -1; //Seting to -1 moves backwards
                    isIdle = true;
                    continue;
                    //Move = "backward";
                }
            }
            else if (move == "backward")
            {
                if (movingTo <= goingTo)
                {
                    isIdle = true;
                    movementDirection = 1; //Seting to 1 moves forward
                    move = "forward";
                    continue;
                }
                //Else if you are at the end of your path
                else if (movingTo >= goingTo + 1)
                {
                    isIdle = false;
                    movementDirection = -1; //Seting to -1 moves backwards
                    //Debug.Log("Moving to 2 = " + movingTo);
                }
            }
            movingTo = movingTo + movementDirection;
            //Debug.Log("Moving to 3 = " + movingTo);
        }
    }
    #endregion //Coroutines
}
