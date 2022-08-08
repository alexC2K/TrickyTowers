using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] Blocks;
    public GameObject NextBlock;
    public GameObject CurrentBlock;
    
    void Start()
    {
        SpawnBlock();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpawnBlock();
        }
    }

    public void SpawnBlock()
    {
        // This way I can show on the UI the next block that will be spawned
        int index = Random.Range(0, Blocks.Length);
        if (CurrentBlock == null)
        {
            CurrentBlock = Instantiate(Blocks[index], transform.position, Quaternion.identity);

            int indexNextBlock = Random.Range(0, Blocks.Length);
            NextBlock = Blocks[indexNextBlock];
            
        }
        else
        {
            Instantiate(NextBlock, transform.position, Quaternion.identity);
            CurrentBlock = null;
        }

        GameManager.Instance.SpawnedBlocks++;
        GameManager.Instance.RemainingBlocks--;
    }

    public Transform GetSpawnerPosition()
    {
        return gameObject.transform;
    }

    public void MoveSpawnerUp()
    {
        gameObject.transform.position += new Vector3(0, 1, 0);
    }
}
