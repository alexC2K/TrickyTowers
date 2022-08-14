using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject CameraObject;
    public GameObject LineObject;
        
    public bool MoveTheCamera = false;
    public bool Moving = false;
    public static Vector3 TargetPosition;

    void Update()
    {
        if (!MoveTheCamera && !Moving)
            return;

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, TargetPosition, Time.deltaTime * 10);
        Moving = true;
        
        if(Vector3.Distance(gameObject.transform.position, TargetPosition) < 0.01f)
        {
            Moving = false;
        }
    }
    
    public void MoveCameraUp()
    {
        TargetPosition = gameObject.transform.position + Vector3.up;

        // Move the line
        Vector3 LinePosition = LineObject.transform.position;
        LinePosition.y += 5f;
        LineObject.transform.position = LinePosition;
        
        MoveTheCamera = true;
    }

    public void AdjustCamera()
    {
        Camera.main.orthographicSize = CameraObject.transform.position.y + 11;
    }
}
