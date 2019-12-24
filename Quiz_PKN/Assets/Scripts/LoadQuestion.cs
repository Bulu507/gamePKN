using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadQuestion : MonoBehaviour
{


    #region Enums

    #endregion //Enums

    #region Public Variables

    List<Question> question = new List<Question>();

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
                q.answer = row[6];
                int.TryParse(row[7], out q.status);

                question.Add(q);
            }
        }

        foreach (Question q in question)
        {
            Debug.Log(q.question);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion //Main Methods

    //(Custom Named Methods)
    #region Utility Methods 

    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines

    #endregion //Coroutines
}
