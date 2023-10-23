using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_NV : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Scene_NV");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
