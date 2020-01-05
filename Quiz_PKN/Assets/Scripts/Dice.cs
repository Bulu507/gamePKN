using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    #region Enums

    #endregion //Enums

    #region Public Variables

    public LoadQuestion TheQuestion;
    public Sprite[] diceSide;
    public static GameObject p1_panel, p2_panel;

    #endregion //Public Variables

    #region Private Variables

    private GameObject[] Players;
    private SpriteRenderer rend;
    private int whosTurn = 1;
    private bool coroutineAllowed = true;

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

        Players = GameObject.FindGameObjectsWithTag("Player");
        p1_panel = GameObject.Find("P1_on");
        p2_panel = GameObject.Find("P2_on");
        p1_panel.SetActive(true);
        p2_panel.SetActive(false);

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
        if (GameController.IsFirstPlay == 0)
        {
            GameController.IsFirstPlay = 1;
        }
        else if (coroutineAllowed)
        {
            StartCoroutine("RollTheDice");
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

        if (LoadQuestion.QuestionAnswer == true)
        {
            GameController.diceSideThrown = randomDiceSide + 1;
        }
        else
        {
            GameController.diceSideThrown = 0;
        }

        switch (whosTurn)
        {
            case 1:
                GameController.MovedPlayer(1);
                p1_panel.SetActive(true);
                p2_panel.SetActive(false);
                break;
            case 2:
                GameController.MovedPlayer(2);
                p1_panel.SetActive(true);
                p2_panel.SetActive(false);
                break;
            case 3:
                GameController.MovedPlayer(3);
                p1_panel.SetActive(true);
                p2_panel.SetActive(false);
                break;
            case 4:
                GameController.MovedPlayer(4);
                p1_panel.SetActive(true);
                p2_panel.SetActive(false);
                break;
        }

        whosTurn += 1;
        if (whosTurn >= Players.Length + 1)
        {
            whosTurn = 1;
        }
        coroutineAllowed = true;

    }

    #endregion //Coroutines


}
