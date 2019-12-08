using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    Node[] pathNode;
    GameObject [] player;
    public float moveSpeed;
    float timer;
    int currentNode;
    static Vector3 currentPositionHolder;
    public int diceRoll;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        pathNode = GetComponentsInChildren<Node>();
        
        //foreach (Node n in pathNode)
        //{
        //    Debug.Log(n.name);
        //}
        CheckNode();
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
        Debug.Log(currentNode);
        MoveForward();
    }

    void CheckNode()
    {
        if (currentNode < (diceRoll))
        {
            timer = 0;
            currentPositionHolder = pathNode[currentNode].transform.position;
        }
    }

    void MoveForward()
    {
        timer += Time.deltaTime * moveSpeed;
        foreach (GameObject g in player)
        {
            if (g.transform.position != currentPositionHolder)
            {
                g.transform.position = Vector3.Lerp(g.transform.position, currentPositionHolder, timer);
            }
            else
            {
                if (currentNode < (diceRoll))
                {
                    currentNode++;
                    CheckNode();
                }
            }
        }
    }

    void DrawLine()
    {
        for(int i = 0; i < pathNode.Length; i++)
        {

            if (i < (pathNode.Length-1))
            {
                Debug.DrawLine(pathNode[i].transform.position, pathNode[i + 1].transform.position, Color.green);
            }
        }
    }
}
