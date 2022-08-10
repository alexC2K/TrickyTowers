using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI PlayerScore;
    public TextMeshProUGUI PlayerMovesRemaining;
    public TextMeshProUGUI PlayerHighscoreUI;

    public Image NextBlock;
    
    public int SpawnedBlocks = 0;
    public int RemainedBlocks = 5;
    public int LastRemainedBlocks = 5;
    public int PlayerHighscore;

    public void Awake()
    {
        if (PlayerPrefs.HasKey("PlayerHighscore"))
        {
            PlayerHighscore = PlayerPrefs.GetInt("PlayerHighscore");
        }
        else
        {
            PlayerPrefs.SetInt("PlayerHighscore", 0);
        }
    }

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            // Restart the level
            SceneManager.LoadScene("EndlessLevel");
        }
    }

    public void Start()
    {
        BlockSpawner.OnBlockSpawned += UpdateScore;
    }

    public void OnDestroy()
    {
        BlockSpawner.OnBlockSpawned -= UpdateScore;
    }
    
    public void UpdateScore()
    {
        SpawnedBlocks += 1;
        RemainedBlocks -= 1;
        
        // update player highscore
        if(SpawnedBlocks > PlayerHighscore)
        {
            PlayerPrefs.SetInt("PlayerHighscore", SpawnedBlocks);
            PlayerHighscore = SpawnedBlocks;
        }

        PlayerScore.text = "Score: " + SpawnedBlocks.ToString();
        PlayerHighscoreUI.text = "Highscore: " + PlayerHighscore;
        PlayerMovesRemaining.text = RemainedBlocks.ToString();
        NextBlock.sprite = FindObjectOfType<BlockSpawner>().GetNextBlockSprite();
    }

    public int GetRemainedBlocks()
    {
        return RemainedBlocks;
    }
    
    public void SetRemainedBlocks()
    {
        RemainedBlocks = LastRemainedBlocks + UnityEngine.Random.Range(1, 10);
        LastRemainedBlocks = RemainedBlocks;
    }

    public void SetLastRemainedBlocks()
    {
        LastRemainedBlocks = RemainedBlocks;
    }
}
