using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    #region Enums

    #endregion //Enums

    #region Public Variables

    public Sprite[] diceSide;
    public static GameObject p1_panel, p2_panel;
    public static int whosTurn = 1;
    public static bool coroutineAllowed = true;

    #endregion //Public Variables

    #region Private Variables

    private GameObject[] Players;
    private SpriteRenderer rend;

    #endregion //Private Variables

    #region Setter Getter

    #endregion //Setter Getter

    // (Unity Named Methods)
    #region Main Methods

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        //diceSide = Resources.LoadAll<Sprite>("Assets/Resources/Dice/");
        rend.sprite = diceSide[0];

    }

    // Update is called once per frame
    void Update()
    {
    }

    #endregion //Main Methods

    //(Custom Named Methods)
    #region Utility Methods 

    private void OnMouseDown()
    {
        if (coroutineAllowed)
        {
            if (GameController.PlayCondition == 0)
            {
                GameController.PlayCondition = 1;
                LoadQuestion.IsCountDown = true;
                LoadQuestion.timeleft = 31f;
            }
            else if (GameController.PlayCondition == 2)
            {
                StartCoroutine("RollTheDice");
            }
        }

    }

    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines

    private IEnumerator RollTheDice()
    {
        coroutineAllowed = false;
        int randomDiceSide = 0;
        
        for (int i =0; i < 15; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSide[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        GameController.diceSideThrown = randomDiceSide + 1;

        switch (whosTurn)
        {
            case 1:
                GameController.MovedPlayer(1);
                break;
            case 2:
                GameController.MovedPlayer(2);
                break;
            case 3:
                GameController.MovedPlayer(3);
                break;
            case 4:
                GameController.MovedPlayer(4);
                break;
        }

        whosTurn += 1;
        if (whosTurn > GameController.Players)
        {
            whosTurn = 1;
        }
        
        GameController.PlayCondition = 0;
        GameController.SetActivePlayer(whosTurn);

        yield return new WaitForSeconds(3);
        coroutineAllowed = true;
    }

    #endregion //Coroutines


}
