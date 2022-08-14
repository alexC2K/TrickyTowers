using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public AudioSource audioSources;
    
    public void LoadFirstLevel()
    {
        // Load the endless level
        SceneManager.LoadScene("EndlessLevel");
    }

    public void ReturnToMainMenu()
    {
        // Load main menu
        SceneManager.LoadScene("MainMenu");
    }
    
    public void QuitGame()
    {
        // Quit the title
        Application.Quit();
    }

    public void PlaySound()
    {
        // Play button SFX
        audioSources.Play();
    }
}
