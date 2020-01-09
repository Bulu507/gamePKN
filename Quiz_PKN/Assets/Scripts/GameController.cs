using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Enums

    #endregion //Enums

    #region Public Variables

    public static GameObject player1, player2, player3, player4;
    public static int playerPlay;
    public static int diceSideThrown;
    public static int PlayCondition;
    public static int Players;
    public static string GameMode;
    public static GameObject p1_panel, p2_panel, p3_panel, p4_panel;
    public static GameObject p1_on, p2_on, p3_on, p4_on;
    public static bool IsFinish;

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
        player3 = GameObject.Find("P3");
        player4 = GameObject.Find("P4");

        p1_on = GameObject.Find("P1_on");
        p2_on = GameObject.Find("P2_on");
        p3_on = GameObject.Find("P3_on");
        p4_on = GameObject.Find("P4_on");

        p1_panel = GameObject.Find("p1_panel");
        p2_panel = GameObject.Find("p2_panel");

        Players = Menu.TotalPlayers;
        GameMode = Menu.Mode;

        SetPlayer(Players);
        GameController.SetActivePlayer(1);

        //Set Condition When Play
        /* condition 0 = do nothing
         * condition 1 = show question
         * condition 2 = allow to roll dice
        */
        PlayCondition = 0;
        IsFinish = false;
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
        playerPlay = playerToMove;
        switch (playerToMove)
        {
            case 1:
                player1.GetComponent<FollowPath>().diceValue = diceSideThrown;
                break;
            case 2:
                player2.GetComponent<FollowPath>().diceValue = diceSideThrown;
                break;
            case 3:
                player3.GetComponent<FollowPath>().diceValue = diceSideThrown;
                break;
            case 4:
                player4.GetComponent<FollowPath>().diceValue = diceSideThrown;
                break;
        }

    }

    private void SetPlayer(int totalPlayer)
    {
        switch (totalPlayer)
        {
            case 2:
                p3_panel = GameObject.Find("p3_panel");
                p4_panel = GameObject.Find("p4_panel");
                player3.GetComponent<SpriteRenderer>().enabled = false;
                player4.GetComponent<SpriteRenderer>().enabled = false;
                p3_on.GetComponent<SpriteRenderer>().enabled = false;
                p4_on.GetComponent<SpriteRenderer>().enabled = false;
                p3_panel.SetActive(false);
                p4_panel.SetActive(false);
                break;
            case 3:
                p3_panel = GameObject.Find("p3_panel");
                p4_panel = GameObject.Find("p4_panel");
                player4.GetComponent<SpriteRenderer>().enabled = false;
                p4_on.GetComponent<SpriteRenderer>().enabled = false;
                p4_panel.SetActive(false);
                break;
            case 4:
                p3_panel = GameObject.Find("p3_panel");
                p4_panel = GameObject.Find("p4_panel");
                break;
        }
    }

    public static void SetActivePlayer(int playerSet)
    {
        switch (Players)
        {
            case 2:
                if(playerSet == 1)
                {
                    p1_on.GetComponent<SpriteRenderer>().enabled = true;
                    p2_on.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    p1_on.GetComponent<SpriteRenderer>().enabled = false;
                    p2_on.GetComponent<SpriteRenderer>().enabled = true;
                }
                break;
            case 3:
                if (playerSet == 1)
                {
                    p1_on.GetComponent<SpriteRenderer>().enabled = true;
                    p2_on.GetComponent<SpriteRenderer>().enabled = false;
                    p3_on.GetComponent<SpriteRenderer>().enabled = false;
                }
                else if (playerSet == 2)
                {
                    p1_on.GetComponent<SpriteRenderer>().enabled = false;
                    p2_on.GetComponent<SpriteRenderer>().enabled = true;
                    p3_on.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    p1_on.GetComponent<SpriteRenderer>().enabled = false;
                    p2_on.GetComponent<SpriteRenderer>().enabled = false;
                    p3_on.GetComponent<SpriteRenderer>().enabled = true;
                }
                break;
            case 4:
                if (playerSet == 1)
                {
                    p1_on.GetComponent<SpriteRenderer>().enabled = true;
                    p2_on.GetComponent<SpriteRenderer>().enabled = false;
                    p3_on.GetComponent<SpriteRenderer>().enabled = false;
                    p4_on.GetComponent<SpriteRenderer>().enabled = false;
                }
                else if (playerSet == 2)
                {
                    p1_on.GetComponent<SpriteRenderer>().enabled = false;
                    p2_on.GetComponent<SpriteRenderer>().enabled = true;
                    p3_on.GetComponent<SpriteRenderer>().enabled = false;
                    p4_on.GetComponent<SpriteRenderer>().enabled = false;
                }
                else if (playerSet == 3)
                {
                    p1_on.GetComponent<SpriteRenderer>().enabled = false;
                    p2_on.GetComponent<SpriteRenderer>().enabled = false;
                    p3_on.GetComponent<SpriteRenderer>().enabled = true;
                    p3_on.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    p1_on.GetComponent<SpriteRenderer>().enabled = false;
                    p2_on.GetComponent<SpriteRenderer>().enabled = false;
                    p3_on.GetComponent<SpriteRenderer>().enabled = false;
                    p4_on.GetComponent<SpriteRenderer>().enabled = true;
                }
                break;
        }
    }
    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines

    #endregion //Coroutines
}
