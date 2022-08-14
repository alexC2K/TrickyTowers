using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public static event Action OnBlockSpawned = delegate { };

    public GameObject[] Blocks;

    [SerializeField] GameObject NextBlock;
    [SerializeField] GameObject CurrentBlock;
    [SerializeField] GameObject LastBlock;
    
    void Start()
    {
        SpawnBlock();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //SpawnBlock();
        }
    }

    public void SpawnBlock()
    {  
        // This way I can show on the UI the next block that will be spawned
        if (NextBlock == null)
        {
            int index = UnityEngine.Random.Range(0, Blocks.Length);
            CurrentBlock = Instantiate(Blocks[index], transform.position, Quaternion.identity);

            int indexNextBlock = UnityEngine.Random.Range(0, Blocks.Length);
            NextBlock = Blocks[indexNextBlock];
        }
        else
        {
            LastBlock = CurrentBlock;
            CurrentBlock = Instantiate(NextBlock, transform.position, Quaternion.identity);

            int indexNextBlock = UnityEngine.Random.Range(0, Blocks.Length);
            NextBlock = Blocks[indexNextBlock];
        }

        // Simple trick for Shape1 (cube) block so it can fit with the other blocks
        if(CurrentBlock.gameObject.name.Contains("Shape1"))
        {
            Vector3 position = CurrentBlock.transform.position;
            position.x += 0.5f;
            
            CurrentBlock.transform.position = position;    
        }

        OnBlockSpawned();
    }

    public Transform GetSpawnerPosition()
    {
        return gameObject.transform;
    }
    
    
    public Sprite GetNextBlockSprite()
    {
        if (NextBlock == null)
            return CurrentBlock.GetComponent<BlockSpriteController>().BlockSprite;
        
        return NextBlock.GetComponent<BlockSpriteController>().BlockSprite;
    }
    
    public void MoveSpawnerUp()
    {
        gameObject.transform.position += new Vector3(0, 3, 0);
    }

    public GameObject GetLastBlock()
    {
        return LastBlock;
    }
}
