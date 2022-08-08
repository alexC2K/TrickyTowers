using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI PlayerScore;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerScore.text = "Score: " + GameManager.Instance.SpawnedBlocks.ToString();
    }
}
