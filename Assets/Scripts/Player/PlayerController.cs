using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI PlayerScore;
    public TextMeshProUGUI PlayerMovesRemaining;
    public Image NextBlock;
    
    public int SpawnedBlocks = 0;
    public int RemainedBlocks = 5;
    public int LastRemainedBlocks = 5;

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
        
        PlayerScore.text = "Score: " + SpawnedBlocks.ToString();
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
