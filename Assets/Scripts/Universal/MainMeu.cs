using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMeu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Cutscene");
    }

    public void QuiGame()
    {
        Application.Quit();
    }
}
