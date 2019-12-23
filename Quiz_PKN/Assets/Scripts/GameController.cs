using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Enums

    #endregion //Enums

    #region Public Variables

    public static GameObject player1, player2;

    public static int diceSideThrown = 0;

    #endregion //Public Variables

    #region Private Variables

    #endregion //Private Variables

    #region Setter Getter

    #endregion //Setter Getter

    // (Unity Named Methods)
    #region Main Methods

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("P1");
        player2 = GameObject.Find("P2");
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion //Main Methods

    //(Custom Named Methods)
    #region Utility Methods 

    public static void MovedPlayer(int playerToMove)
    {
        switch (playerToMove)
        {
            case 1:
                player1.GetComponent<FollowPath>().diceValue = diceSideThrown;
                break;
            case 2:
                player2.GetComponent<FollowPath>().diceValue = diceSideThrown;
                break;
        }
    }

    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines

    #endregion //Coroutines
}
