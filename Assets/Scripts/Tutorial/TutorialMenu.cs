using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadScene("Pemilihan Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
