using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public CanvasGroup EndGamePanel;
    public Canvas UI;
    public TextMeshProUGUI PlayerScore;
    public TextMeshProUGUI PlayerHighscore;
    
    public bool GameOver = false;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void EndLevel()
    {
        GameOver = true;
        
        FindObjectOfType<CameraController>().AdjustCamera();

        // Show the end game panel after 10s so the player can see their tower
        StartCoroutine(ShowEndGamePanel());
    }

    IEnumerator ShowEndGamePanel()
    {
        yield return new WaitForSeconds(10f);

        UI.gameObject.SetActive(false);

        PlayerScore.text = "Score: " + FindObjectOfType<PlayerController>().SpawnedBlocks;
        PlayerHighscore.text = "Highscore: " + FindObjectOfType<PlayerController>().PlayerHighscore;
        
        EndGamePanel.alpha = 1;
        EndGamePanel.interactable = true;
    }
}
