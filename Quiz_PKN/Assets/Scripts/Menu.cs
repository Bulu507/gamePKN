using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static string Mode;
    public static int TotalPlayers;

    public void SetMode(string mode)
    {
        Mode = mode;
        Debug.Log("mode = " + Mode);
    }

    public void SetPlayers(int players)
    {
        TotalPlayers = players;
        Debug.Log("players = " + TotalPlayers);
    }

}
