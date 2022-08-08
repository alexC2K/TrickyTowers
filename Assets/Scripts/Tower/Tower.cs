using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public static Tower Instance;
    
    public GameObject TowerObject;

    public float MaxHeight;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        MaxHeight = TowerObject.transform.position.y;
    }
    
    public Transform GetTowerPosition()
    {
        return TowerObject.transform;
    }
}
