using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // pindah scene ke menu
    public void MenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // pindah scene kellading
    public void LoadingScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    // keluar dari game
    public void KeluarScene()
    {
        Application.Quit();
    }

}
