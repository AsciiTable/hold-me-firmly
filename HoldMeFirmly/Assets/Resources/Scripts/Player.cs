using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float CIRCLE_DEGREES = 360.0f;
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float tiltSpeed = 5.0f;
    [SerializeField] private float maxTilt = 30.0f;
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
        if (Input.GetButton("Tilt"))
            TiltPlayer();
    }

    private void MovePlayer() {
        if(Input.GetAxis("Horizontal") > 0)
            gameObject.transform.position += new Vector3(movementSpeed * Time.deltaTime, 0.0f, 0.0f);
        else
            gameObject.transform.position += new Vector3(-1.0f * movementSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    // would want strap underneath plate to prevent it from falling off from hand
    private void TiltPlayer() {
        float eulerZ = gameObject.transform.localEulerAngles.z;
        if (Input.GetAxis("Tilt") > 0) { // Tilt to the left
            if (eulerZ < maxTilt || (eulerZ > maxTilt && eulerZ >= CIRCLE_DEGREES - maxTilt - 0.01f)) {
                gameObject.transform.localEulerAngles += new Vector3(0.0f, 0.0f, tiltSpeed * Time.deltaTime);
                if (gameObject.transform.localEulerAngles.z > maxTilt && gameObject.transform.localEulerAngles.z < CIRCLE_DEGREES - maxTilt) {
                    gameObject.transform.localEulerAngles = new Vector3(0.0f, 0.0f, maxTilt);
                }
            }
        }
        else if (Input.GetAxis("Tilt") < 0) { // Tilt to the right
            if ((eulerZ > CIRCLE_DEGREES - maxTilt) || (eulerZ <= maxTilt + 0.01f)) {
                gameObject.transform.localEulerAngles += new Vector3(0.0f, 0.0f, -1.0f * tiltSpeed * Time.deltaTime);
                if (gameObject.transform.localEulerAngles.z < CIRCLE_DEGREES - maxTilt && gameObject.transform.localEulerAngles.z > maxTilt){
                    gameObject.transform.localEulerAngles = new Vector3(0.0f, 0.0f, CIRCLE_DEGREES - maxTilt);
                }
            }
        }
    }
}
