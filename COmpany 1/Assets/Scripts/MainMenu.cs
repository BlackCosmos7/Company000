using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayNovell()
    {
        SceneManager.LoadScene("novell");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}