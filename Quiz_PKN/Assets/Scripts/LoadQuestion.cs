using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class LoadQuestion : MonoBehaviour
{


    #region Enums

    #endregion //Enums

    #region Public Variables

    GameObject[] Path;
    public static bool QuestionAnswer;
    public static bool IsCountDown;
    public static float timeleft;

    #endregion //Public Variables

    #region Private Variables

    private List<Question> question = new List<Question>();
    private Question currentQuestion;
    private Text QText, timerTxt, answerA, answerB, answerC, answerD;
    private Text pointP1, pointP2, pointP3, pointP4;
    private Button btnA, btnB, btnC, btnD;
    private SpriteRenderer QBoard, TBoard;
    private GameObject player1, player2, dice;
    private int randQuestIndex;

    #endregion //Private Variables

    #region Setter Getter



    #endregion //Setter Getter

    // (Unity Named Methods)
    #region Main Methods

    // Start is called before the first frame update
    void Start()
    {
        if (question.Count == 0)
        {
            loadQuest();
        }

        Path = GameObject.FindGameObjectsWithTag("Path");
        QText = GameObject.Find("question_text").GetComponent<Text>();
        QBoard = GameObject.Find("question_board").GetComponent<SpriteRenderer>();
        TBoard = GameObject.Find("Timer_board").GetComponent<SpriteRenderer>();
        timerTxt = GameObject.Find("Timer_answer").GetComponent<Text>();
        btnA = GameObject.Find("ButtonA").GetComponent<Button>();
        btnB = GameObject.Find("ButtonB").GetComponent<Button>();
        btnC = GameObject.Find("ButtonC").GetComponent<Button>();
        btnD = GameObject.Find("ButtonD").GetComponent<Button>();
        answerA = GameObject.Find("btnA_txt").GetComponent<Text>();
        answerB = GameObject.Find("btnB_txt").GetComponent<Text>();
        answerC = GameObject.Find("btnC_txt").GetComponent<Text>();
        answerD = GameObject.Find("btnD_txt").GetComponent<Text>();
        pointP1 = GameObject.Find("P1_point").GetComponent<Text>();
        pointP2 = GameObject.Find("P2_point").GetComponent<Text>();

        player1 = GameObject.Find("P1");
        player2 = GameObject.Find("P2");
        dice = GameObject.Find("Dice");

        CekSprite();

        EnableQuestion(false);

        QuestionAnswer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.PlayCondition == 1)
        {
            ShowQuestion();
        }

        if(IsCountDown)
        {
            AnswerCountDown();
        }

        Debug.Log("Countdown = " + IsCountDown);

        if (Input.GetKeyDown("space"))
        {
            EnableQuestion(false);
        }

    }

    #endregion //Main Methods

    //(Custom Named Methods)
    #region Utility Methods 

    private void loadQuest()
    {
        TextAsset questionData = Resources.Load<TextAsset>("question");
        string[] data = questionData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ';' });

            if (row[1] != "")
            {
                Question q = new Question();
                int.TryParse(row[0], out q.id);
                q.question = row[1];
                q.choiceA = row[2];
                q.choiceB = row[3];
                q.choiceC = row[4];
                q.choiceD = row[5];
                int.TryParse(row[6], out q.answer);

                question.Add(q);
            }
        }
    }

    private void EnableQuestion(bool visible)
    {
        QText.enabled = visible;
        TBoard.enabled = visible;
        timerTxt.enabled = visible;
        QBoard.enabled = visible;
        btnA.enabled = visible;
        btnB.enabled = visible;
        btnC.enabled = visible;
        btnD.enabled = visible;
        answerA.enabled = visible;
        answerB.enabled = visible;
        answerC.enabled = visible;
        answerD.enabled = visible;
        btnA.image.enabled = visible;
        btnB.image.enabled = visible;
        btnC.image.enabled = visible;
        btnD.image.enabled = visible;
    }

    public void VisiblePlayer(bool b)
    {
        player1.GetComponent<SpriteRenderer>().enabled = b;
        player2.GetComponent<SpriteRenderer>().enabled = b;
        dice.SetActive(b);
    }

    public void CekSprite()
    {
        switch (GameController.Players)
        {
            case 3:
                pointP3 = GameObject.Find("P3_point").GetComponent<Text>();
                break;
            case 4:
                pointP3 = GameObject.Find("P3_point").GetComponent<Text>();
                pointP4 = GameObject.Find("P4_point").GetComponent<Text>();
                break;
        }
    }

    public void ShowQuestion()
    {
        GameController.PlayCondition = 0;
        GameController.playerPlay = 0;

        VisiblePlayer(false);

        randQuestIndex = Random.Range(0, question.Count);

        EnableQuestion(true);

        QText.text = question[randQuestIndex].question;
        answerA.text = question[randQuestIndex].choiceA;
        answerB.text = question[randQuestIndex].choiceB;
        answerC.text = question[randQuestIndex].choiceC;
        answerD.text = question[randQuestIndex].choiceD;
    }

    public void CekQuestion(int questIndex, int choice)
    {
        GameController.PlayCondition = 0;

        int answer = question[questIndex].answer;

        if (answer == choice)
        {
            Debug.Log("Jawaban Benar");
            QuestionAnswer = true;
            EnableQuestion(false);
            VisiblePlayer(true);
            GameController.PlayCondition = 2;
        }
        else
        {
            Debug.Log("Jawaban Salah");
            QuestionAnswer = false;
            EnableQuestion(false);
            VisiblePlayer(true);

            Dice.whosTurn += 1;
            if (Dice.whosTurn > GameController.Players)
            {
                Dice.whosTurn = 1;
            }
            GameController.SetActivePlayer(Dice.whosTurn);
            Dice.coroutineAllowed = true;

            GameController.PlayCondition = 0;
        }

        question.RemoveAt(questIndex);
        if (question.Count == 0)
        {
            loadQuest();
        }

        //answer = 0;
    }

    public void SetBtnAnswer(int a)
    {
        IsCountDown = false;
        CekQuestion(randQuestIndex, a);
    }

    public void AnswerCountDown()
    {
        timeleft -= Time.deltaTime;
        timerTxt.text = Mathf.Round(timeleft).ToString();
        if (timeleft < 0)
        {
            IsCountDown = false;
            CekQuestion(randQuestIndex, 0);
        }
    }

    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines

    #endregion //Coroutines
}
