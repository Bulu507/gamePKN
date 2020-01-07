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
    public int answer;

    #endregion //Public Variables

    #region Private Variables

    private List<Question> question = new List<Question>();
    private Question currentQuestion;
    private Text QText, answerA, answerB, answerC, answerD;
    private Text pointP1, pointP2, pointP3, pointP4;
    private Button btnA, btnB, btnC, btnD;
    private SpriteRenderer QBoard;
    private GameObject player1, player2, dice;

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
        pointP3 = GameObject.Find("P3_point").GetComponent<Text>();
        pointP4 = GameObject.Find("P4_point").GetComponent<Text>();
        player1 = GameObject.Find("P1");
        player2 = GameObject.Find("P2");
        dice = GameObject.Find("Dice");

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
        player1.SetActive(b);
        player2.SetActive(b);
        dice.SetActive(b);
    }

    public void ShowQuestion()
    {
        GameController.PlayCondition = 3;
        GameController.playerPlay = 0;

        VisiblePlayer(false);

        int randQuestIndex = Random.Range(0, question.Count);

        EnableQuestion(true);

        QText.text = question[randQuestIndex].question;
        answerA.text = question[randQuestIndex].choiceA;
        answerB.text = question[randQuestIndex].choiceB;
        answerC.text = question[randQuestIndex].choiceC;
        answerD.text = question[randQuestIndex].choiceD;

        CekQuestion(randQuestIndex);

    }

    public void CekQuestion(int questIndex)
    {
         answer = 0;
        int ans = question[questIndex].answer;

        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "ButtonA":
                answer = 1;
                break;
            case "ButtonB":
                answer = 2;
                break;
            case "ButtonC":
                answer = 3;
                break;
            case "ButtonD":
                answer = 4;
                break;
        }

        if (ans == answer || answer == 3 || answer == 4)
        {
            Debug.Log("Jawaban Benar");
            QuestionAnswer = true;
            EnableQuestion(false);
            VisiblePlayer(true);
        }
        else
        {
            Debug.Log("Jawaban Salah");
            QuestionAnswer = false;
            EnableQuestion(false);
            VisiblePlayer(true);
            GameController.PlayCondition = 0;
            Dice.whosTurn += 1;
            if (Dice.whosTurn > GameController.Players)
            {
                Dice.whosTurn = 1;
            }
            GameController.SetActivePlayer(Dice.whosTurn);
            Dice.coroutineAllowed = true;
        }

        
        question.RemoveAt(questIndex);
        if (question.Count == 0)
        {
            loadQuest();
        }
    }



    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines


    #endregion //Coroutines
}
