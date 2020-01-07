using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // pindah scene ke menu
    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
    }


    // keluar dari game
    public void KeluarScene()
    {
        Application.Quit();
    }

}
