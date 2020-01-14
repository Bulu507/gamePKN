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
    public static bool IsCountDown;
    public static float timeleft;
    public GameObject answerTrue, answerFalse;
    public AudioSource benar;
    public AudioSource Salah;

    #endregion //Public Variables

    #region Private Variables

    private List<Question> question = new List<Question>();
    private List<Essay> essay = new List<Essay>();
    private Question currentQuestion;
    private Text QText, timerTxt, answerA, answerB, answerC, answerD;
    private Text pointP1Txt, pointP2Txt, pointP3Txt, pointP4Txt;
    private int pointP1, pointP2, pointP3, pointP4;
    private Button btnA, btnB, btnC, btnD, btnTrue, btnFalse;
    private Image btnTrueImg, btnFalseImg;
    private SpriteRenderer QBoard, TBoard;
    private GameObject player1, player2, player3, player4, dice;
    private int randQuestIndex;
    private bool IsFirstPlay;
    private bool IsEssayTrue;
    

    #endregion //Private Variables

    #region Setter Getter

    #endregion //Setter Getter

    // (Unity Named Methods)
    #region Main Methods

    // Start is called before the first frame update
    void Start()
    {
        if (GameController.GameMode == "Multiple")
        {
            if (question.Count == 0)
            {
                loadQuest();
            }
        }
        else if (GameController.GameMode == "Essay")
        {
            if (essay.Count == 0)
            {
                loadEssay();
            }
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
        btnTrue = GameObject.Find("BtnTrue").GetComponent<Button>();
        btnFalse = GameObject.Find("BtnFalse").GetComponent<Button>();
        btnTrueImg = GameObject.Find("BtnTrue").GetComponent<Image>();
        btnFalseImg = GameObject.Find("BtnFalse").GetComponent<Image>();
        answerA = GameObject.Find("btnA_txt").GetComponent<Text>();
        answerB = GameObject.Find("btnB_txt").GetComponent<Text>();
        answerC = GameObject.Find("btnC_txt").GetComponent<Text>();
        answerD = GameObject.Find("btnD_txt").GetComponent<Text>();
        pointP1Txt = GameObject.Find("P1_point").GetComponent<Text>();
        pointP2Txt = GameObject.Find("P2_point").GetComponent<Text>();

        player1 = GameObject.Find("P1");
        player2 = GameObject.Find("P2");

        pointP1 = 0;
        pointP2 = 0;
        pointP1Txt.text = pointP1.ToString();
        pointP2Txt.text = pointP2.ToString();

        dice = GameObject.Find("Dice");

        answerTrue.SetActive(false);
        answerFalse.SetActive(false);

        CekSprite();

        IsFirstPlay = true;
        EnableQuestion(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.PlayCondition == 1)
        {
            ShowQuestion();
        }

        if (IsCountDown)
        {
            AnswerCountDown();
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

    private void loadEssay()
    {
        TextAsset questionData = Resources.Load<TextAsset>("essay");
        string[] data = questionData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ';' });

            if (row[1] != "")
            {
                Essay q = new Essay();
                int.TryParse(row[0], out q.id);
                q.question = row[1];
                essay.Add(q);
            }

            foreach(Essay e in essay)
            {
                Debug.Log(e.question);
            }
        }
    }

    private void EnableQuestion(bool visible)
    {
        QText.enabled = visible;
        QBoard.enabled = visible;

        if (IsFirstPlay)
        {
            TBoard.enabled = visible;
            timerTxt.enabled = visible;
            answerA.enabled = visible;
            answerB.enabled = visible;
            answerC.enabled = visible;
            answerD.enabled = visible;
            btnA.enabled = visible;
            btnB.enabled = visible;
            btnC.enabled = visible;
            btnD.enabled = visible;
            btnA.image.enabled = visible;
            btnB.image.enabled = visible;
            btnC.image.enabled = visible;
            btnD.image.enabled = visible;
            btnTrue.enabled = visible;
            btnFalse.enabled = visible;
            btnTrueImg.enabled = visible;
            btnFalseImg.enabled = visible;
        }
        else
        {
            if (GameController.GameMode == "Multiple")
            {
                TBoard.enabled = visible;
                timerTxt.enabled = visible;
                answerA.enabled = visible;
                answerB.enabled = visible;
                answerC.enabled = visible;
                answerD.enabled = visible;
                btnA.enabled = visible;
                btnB.enabled = visible;
                btnC.enabled = visible;
                btnD.enabled = visible;
                btnA.image.enabled = visible;
                btnB.image.enabled = visible;
                btnC.image.enabled = visible;
                btnD.image.enabled = visible;
            }
            else if (GameController.GameMode == "Essay")
            {
                btnTrue.enabled = visible;
                btnFalse.enabled = visible;
                btnTrueImg.enabled = visible;
                btnFalseImg.enabled = visible;
            }
        }
    }

    public void VisiblePlayer(bool b)
    {
        switch (GameController.Players)
        {
            case 2:
                player1.GetComponent<SpriteRenderer>().enabled = b;
                player2.GetComponent<SpriteRenderer>().enabled = b;
                break;
            case 3:
                player3 = GameObject.Find("P3");

                player1.GetComponent<SpriteRenderer>().enabled = b;
                player2.GetComponent<SpriteRenderer>().enabled = b;
                player3.GetComponent<SpriteRenderer>().enabled = b;
                break;
            case 4:
                player3 = GameObject.Find("P3");
                player4 = GameObject.Find("P4");

                player1.GetComponent<SpriteRenderer>().enabled = b;
                player2.GetComponent<SpriteRenderer>().enabled = b;
                player3.GetComponent<SpriteRenderer>().enabled = b;
                player4.GetComponent<SpriteRenderer>().enabled = b;
                break;
        }
        
        dice.SetActive(b);
    }

    public void CekSprite()
    {
        switch (GameController.Players)
        {
            case 3:
                pointP3Txt = GameObject.Find("P3_point").GetComponent<Text>();
                pointP3 = 0;
                pointP3Txt.text = pointP3.ToString();
                break;
            case 4:
                pointP3Txt = GameObject.Find("P3_point").GetComponent<Text>();
                pointP4Txt = GameObject.Find("P4_point").GetComponent<Text>();

                pointP3 = 0;
                pointP4 = 0;

                pointP3Txt.text = pointP3.ToString();
                pointP4Txt.text = pointP4.ToString();
                break;
        }
    }

    public void ShowQuestion()
    {
        IsFirstPlay = false;
        GameController.PlayCondition = 0;
        GameController.playerPlay = 0;

        VisiblePlayer(false);

        EnableQuestion(true);

        if(GameController.GameMode == "Multiple")
        {
            randQuestIndex = Random.Range(0, question.Count);

            QText.text = question[randQuestIndex].question;
            answerA.text = question[randQuestIndex].choiceA;
            answerB.text = question[randQuestIndex].choiceB;
            answerC.text = question[randQuestIndex].choiceC;
            answerD.text = question[randQuestIndex].choiceD;
        }
        else if (GameController.GameMode == "Essay")
        {
            randQuestIndex = Random.Range(0, essay.Count);
            QText.text = essay[randQuestIndex].question;
        }

    }

    public void CekQuestion(int questIndex, int choice, bool essayAnswer)
    {
        GameController.PlayCondition = 0;
        bool IsAnswerTrue = true;

        if (GameController.GameMode == "Multiple")
        {
            int answer = question[questIndex].answer;

            Debug.Log("jb = " + question[questIndex].answer);

            if (answer == choice)
            {
                Debug.Log("Jawaban Benar");
                IsAnswerTrue = true;
                UpdatePoint(Dice.whosTurn);

                GameController.PlayCondition = 2;
            }
            else
            {
                Debug.Log("Jawaban Salah");
                IsAnswerTrue = false;

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
        }
        else if (GameController.GameMode == "Essay")
        {
            if (essayAnswer)
            {
                Debug.Log("Jawaban Benar");
                IsAnswerTrue = true;
                UpdatePoint(Dice.whosTurn);

                GameController.PlayCondition = 2;
            }
            else
            {
                Debug.Log("Jawaban Salah");
                IsAnswerTrue = false;

                Dice.whosTurn += 1;
                if (Dice.whosTurn > GameController.Players)
                {
                    Dice.whosTurn = 1;
                }
                GameController.SetActivePlayer(Dice.whosTurn);
                Dice.coroutineAllowed = true;

                GameController.PlayCondition = 0;
            }

            essay.RemoveAt(questIndex);
            if (essay.Count == 0)
            {
                loadEssay();
            }
        }

        EnableQuestion(false);
        StartCoroutine(ShowAnswer(IsAnswerTrue));
    }

    public void GetBtnMultipleClick(int a)
    {
        IsCountDown = false;
        CekQuestion(randQuestIndex, a, false);
    }

    public void AnswerCountDown()
    {
        timeleft -= Time.deltaTime;
        timerTxt.text = Mathf.Round(timeleft).ToString();
        if (timeleft < 0)
        {
            IsCountDown = false;
            CekQuestion(randQuestIndex, -1, false);
        }
    }

    public void UpdatePoint(int player)
    {
        switch (player)
        {
            case 1:
                pointP1 += 10;
                pointP1Txt.text = pointP1.ToString();
                break;
            case 2:
                pointP2 += 10;
                pointP2Txt.text = pointP2.ToString();
                break;
            case 3:
                pointP3 += 10;
                pointP3Txt.text = pointP3.ToString();
                break;
            case 4:
                pointP4 += 10;
                pointP4Txt.text = pointP4.ToString();
                break;
        }
    }

    public void GetBtnEssayClick(bool b)
    {
        IsEssayTrue = b;
        CekQuestion(randQuestIndex, 0, b);
    }

    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines

    private IEnumerator ShowAnswer(bool b)
    {
        yield return new WaitForSeconds(0.5f);

        if (b == true)
        {
            answerTrue.SetActive(true);
            benar.Play();
        }
        else
        {
            answerFalse.SetActive(true);
            benar.Play();
        }

        yield return new WaitForSeconds(3);

        answerTrue.SetActive(false);
        answerFalse.SetActive(false);
        
        VisiblePlayer(true);
    }

    #endregion //Coroutines
}
