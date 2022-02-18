using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catchable : MonoBehaviour
{
    [SerializeField] private float ySpawn = 0.0f;
    [SerializeField] private float yDespawn = 0.0f;
    
    [SerializeField] private float dropSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveWithPlatform(float deltaX) {
        gameObject.transform.position += new Vector3(deltaX, 0.0f, 0.0f);
    }
}
