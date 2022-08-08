using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        // Load the endless level
        SceneManager.LoadScene("EndlessLevel");
    }

    public void QuitGame()
    {
        // Quit the title
        Application.Quit();
    }
}
