using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockController : MonoBehaviour
{
    public static Transform TowerPosition;
    public static Transform EndPosition;

    Rigidbody2D BlockRB;
    
    void Awake()
    {
        TowerPosition = Tower.Instance.GetTowerPosition();
        EndPosition = FindObjectOfType<BlockSpawner>().GetSpawnerPosition();
    }

    void Start()
    {
        BlockRB = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Move();
        CheckEndCases();
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
        // TO DO: when the RemainedBlocks = 0, increase them and continue placing the blocks
        if (FindObjectOfType<PlayerController>().RemainedBlocks == 0)
        {
            FindObjectOfType<PlayerController>().SetRemainedBlocks();
        }
        // TO DO: if the line is crossed, end the match
        // TO DO: currently if a piece falls over after placing it, it doesn't count to losing

        // End the level if I reached the line
        if (Tower.Instance.MaxHeight >= EndPosition.position.y - 3 && Tower.Instance.MaxHeight <= EndPosition.position.y)
        {
            FindObjectOfType<CameraController>().MoveCameraUp();
            FindObjectOfType<BlockSpawner>().MoveSpawnerUp();
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
        BlockRB.mass = 1;
        
        this.enabled = false;

        if (Tower.Instance.MaxHeight < transform.position.y)
        {
            Tower.Instance.MaxHeight = transform.position.y;
        }
    }
}
