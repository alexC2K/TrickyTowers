using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockController : MonoBehaviour
{
    public static event Action OnCollisionEnterDetection = delegate { };
    
    public static Transform TowerPosition;
    public static Transform EndPosition;

    static float VELOCITY_MULTIPLICATOR = 5f;

    Rigidbody2D BlockRB;
    bool BlockSpawned;
    
    void Start()
    {
        BlockSpawned = false;
        TowerPosition = Tower.Instance.GetTowerPosition();
        EndPosition = FindObjectOfType<BlockSpawner>().GetSpawnerPosition();
        BlockRB = GetComponent<Rigidbody2D>();

        // Start going down slowly.
        BlockRB.velocity = Vector2.down * VELOCITY_MULTIPLICATOR;
    }
    
    void Update()
    {
        Move();
        CheckEndCases();
        MoveFaster();
    }

    public void Move()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            // Move to left
            transform.Translate(Vector3.left, Space.World);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            // Move to right
            transform.Translate(Vector3.right, Space.World);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            // Rotate Z axis 90 degrees
            transform.Rotate(Vector3.forward, 90);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            // Rotate Z axis -90 degrees
            transform.Rotate(Vector3.forward, -90);
        }
    }

    public void CheckEndCases()
    {
        // When the RemainedBlocks = 0, increase them and continue placing the blocks
        if (FindObjectOfType<PlayerController>().RemainedBlocks == 0)
        {
            FindObjectOfType<PlayerController>().SetRemainedBlocks();
        }
        
        // TO DO: if the line is crossed, end the match

        // End the level if I reached the line
        if (Tower.Instance.MaxHeight >= EndPosition.position.y - 7 && Tower.Instance.MaxHeight <= EndPosition.position.y)
        {
            FindObjectOfType<CameraController>().MoveCameraUp();
            FindObjectOfType<BlockSpawner>().MoveSpawnerUp();
        }
    }
    
    public void MoveFaster()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            BlockRB.velocity = Vector2.down * VELOCITY_MULTIPLICATOR * 3;
        }
        else
        {
            BlockRB.velocity = Vector2.down * VELOCITY_MULTIPLICATOR;
        }
    }
    
    public void EndLevel()
    {
        // Restart the level
        Destroy(gameObject);

        SceneManager.LoadScene("EndlessLevel");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        BlockRB.velocity = Vector2.zero;

        this.enabled = false;
        
        // Update the tower height if necessary
        if (Tower.Instance.MaxHeight < transform.position.y)
        {
            Tower.Instance.MaxHeight = transform.position.y;
        }

        if(!BlockSpawned)
        {
            OnCollisionEnterDetection();
            // Spawn the next block
            FindObjectOfType<BlockSpawner>().SpawnBlock();
            BlockSpawned = true;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        BlockRB.constraints = RigidbodyConstraints2D.None;
    }

    public GameObject GetCurrentBlock()
    {
        return this.gameObject;
    }
}
