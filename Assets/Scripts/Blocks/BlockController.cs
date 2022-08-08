using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockController : MonoBehaviour
{
    public static Transform TowerPosition;
    public static Transform EndPosition;

    void Awake()
    {
        TowerPosition = Tower.Instance.GetTowerPosition();
        EndPosition = FindObjectOfType<BlockSpawner>().GetSpawnerPosition();
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
            transform.Rotate(Vector3.forward, transform.position.z + 90);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            // Rotate Z axis -90 degrees
            transform.Rotate(Vector3.forward, transform.position.z - 90);
        }
    }

    public void CheckEndCases()
    {
        // End the level if I'm under the tower with a block
        if (transform.position.y < TowerPosition.position.y - 5)
        {
            EndLevel();
            return;
        }

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

    public void OnCollisionEnter(Collision collision)
    {
        this.enabled = false;

        if (Tower.Instance.MaxHeight < transform.position.y)
        {
            Tower.Instance.MaxHeight = transform.position.y;
        }
    }
}
