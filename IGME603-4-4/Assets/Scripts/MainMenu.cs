using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("Sam Test");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void Options()
    {
        Debug.Log("options coming soon");
        //SceneManager.LoadScene("Options");
    }

}
