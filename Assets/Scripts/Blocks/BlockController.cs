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
        //LineConnection(); // This works as intended if I'm using sprites.
    }
    
    public void LineConnection()
    {
        // Calculate the position
        Vector3 BlockPosition = this.gameObject.transform.position;
        Vector3 TargetPosition = this.gameObject.transform.position;
        TargetPosition.y = Tower.Instance.gameObject.transform.position.y;
        
        // Set the line
        var lr = this.gameObject.GetComponent<LineRenderer>();
        lr.SetPosition(0, BlockPosition);
        lr.SetPosition(1, TargetPosition);

        // Calculate the width
        float width = this.gameObject.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        lr.startWidth = width;
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
            FindObjectOfType<CameraController>().MoveCameraUp();
            FindObjectOfType<BlockSpawner>().MoveSpawnerUp();
        }
        
        // If the line is crossed, end the match
        if(Tower.Instance.MaxHeight >= FindObjectOfType<CameraController>().LineObject.gameObject.transform.position.y)
        {
            FindObjectOfType<GameController>().EndLevel();
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        BlockRB.velocity = Vector2.zero;

        this.enabled = false;

        /*
        // Remove the line renderer
        var lr = this.gameObject.GetComponent<LineRenderer>();
        lr.enabled = false;
        */
        
        // Update the tower height if necessary
        if (Tower.Instance.MaxHeight < transform.position.y)
        {
            Tower.Instance.MaxHeight = transform.position.y;
        }

        if(!BlockSpawned)
        {
            OnCollisionEnterDetection();

            // Spawn the next block
            if (GameController.Instance.GameOver != true)
            {
                FindObjectOfType<BlockSpawner>().SpawnBlock();
                BlockSpawned = true;
            }
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
