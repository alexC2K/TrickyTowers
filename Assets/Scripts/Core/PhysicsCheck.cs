using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public static Transform TowerPosition;
    
    float speed = 1f;
    float StartTime;

    void Start()
    {
        CheckForTowerPosition();
        StartTime = Time.time;
    }

    void Update()
    {
        if(Time.time - StartTime > 1)
        {
            CheckForTowerPosition();
        }
        
        LoopUpAndDown();
    }
    
    void LoopUpAndDown()
    {
        float yPos = Mathf.PingPong(Time.time * speed, 1) * TowerPosition.position.y;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Block")
            {
                if (hit.rigidbody.bodyType == RigidbodyType2D.Dynamic)
                {
                    hit.rigidbody.AddForce(Vector2.right * 0.01f, ForceMode2D.Force);
                    Debug.Log("Hit a block: " + hit.collider.name);
                }
            }
        }
    }

    void CheckForTowerPosition()
    {
        TowerPosition = Tower.Instance.GetTowerPosition();
    }
}
