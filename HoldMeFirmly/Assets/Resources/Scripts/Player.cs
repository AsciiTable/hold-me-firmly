using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float tiltSpeed = 5.0f;
    [SerializeField] private float energyExpenditureSpeed = 2.0f;
    [SerializeField] private float totalEnergy = 100.0f;
    private float currentEnergy = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = totalEnergy;

    }

    // Update is called once per frame
    void Update(){
        if(Input.GetButton("Horizontal"))
            MovePlayer();
    }

    private void MovePlayer() {
        if(Input.GetAxis("Horizontal") > 0)
            gameObject.transform.position += new Vector3(movementSpeed * Time.deltaTime, 0.0f, 0.0f);
        else
            gameObject.transform.position += new Vector3(-1.0f * movementSpeed * Time.deltaTime, 0.0f, 0.0f);
    }
}
