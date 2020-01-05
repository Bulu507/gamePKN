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

    #endregion //Public Variables

    #region Private Variables

    private  List<Question> question = new List<Question>();
    private  Question currentQuestion;
    private  Text QText, answerA, answerB, answerC, answerD;
    private  Button btnA, btnB, btnC, btnD;
    private  SpriteRenderer QBoard;

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

        EnableQuestion(false);

        QuestionAnswer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.IsFirstPlay == 1)
        {
            GameController.playerPlay = 5;
        }
        switch (GameController.playerPlay)
        {
            case 1:
                if (Path[0].GetComponent<MovementPath>().IsIdle == true && Path[0].GetComponent<MovementPath>().MovingTo != 0)
                {
                    GameController.playerPlay = 5;
                }
                break;
            case 2:
                if (Path[1].GetComponent<MovementPath>().IsIdle == true && Path[1].GetComponent<MovementPath>().MovingTo != 0)
                {
                    GameController.playerPlay = 5;
                }
                break;
            case 5:
                ShowQuestion();
                break;
        }

        if (Input.GetKeyDown("space"))
        {
            EnableQuestion(false);
        }

        Debug.Log(QuestionAnswer);
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

    public void ShowQuestion()
    {
        GameController.IsFirstPlay = 3;
        GameController.playerPlay = 0;

        int randQuestIndex = Random.Range(0, question.Count);

        EnableQuestion(true);

        QText.text = question[randQuestIndex].question;
        answerA.text = question[randQuestIndex].choiceA;
        answerB.text = question[randQuestIndex].choiceB;
        answerC.text = question[randQuestIndex].choiceC;
        answerD.text = question[randQuestIndex].choiceD;

        CekQuestion(randQuestIndex);

        //StartCoroutine(CekQuest(randQuestIndex));

    }

    public void CekQuestion(int questIndex)
    {
        int answer = 0;
        int ans = question[questIndex].answer;
        
        if(EventSystem.current.currentSelectedGameObject.name != null)
        {
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
        }

        if (ans == answer)
        {
            Debug.Log("Jawaban Benar");
            QuestionAnswer = true;
            EnableQuestion(false);
        }
        else
        {
            Debug.Log("Jawaban Salah");
            QuestionAnswer = false;
            EnableQuestion(false);
        }

        
        question.RemoveAt(questIndex);
        if(question.Count == 0)
        {
            loadQuest();
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

    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines

    private IEnumerator CekQuest(int questIndex) 
    {
        int answer = 0;
        int ans = question[questIndex].answer;

        if (EventSystem.current.currentSelectedGameObject.name != null)
        {
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
        }

        if (ans == answer)
        {
            Debug.Log("Jawaban Benar");
            QuestionAnswer = true;
            EnableQuestion(false);
        }
        else
        {
            Debug.Log("Jawaban Salah");
            QuestionAnswer = false;
            EnableQuestion(false);
        }


        question.RemoveAt(questIndex);
        if (question.Count == 0)
        {
            loadQuest();
        }

        yield return new WaitForSeconds(1);
    }

    #endregion //Coroutines
}
