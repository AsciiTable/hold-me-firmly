using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum EnergyExpenditureMode { 
        MultipliedAll,
        DividedAll,
        MultipliedSelected,
        DividedSelected
    };

    private const float CIRCLE_DEGREES = 360.0f;
    [SerializeField] private EnergyExpenditureMode mode = EnergyExpenditureMode.MultipliedAll;
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float tiltSpeed = 5.0f;
    [SerializeField] private float maxTilt = 30.0f;
    [SerializeField] private float energyExpenditureSpeed = 2.0f;
    [SerializeField] private float totalEnergy = 100.0f;
    public static List<Catchable> listOfCaughtShapes = new List<Catchable>();
    private float currentEnergy = 0.0f;

    // Passive Energy Expenditure
    [SerializeField] private float passiveEnergyTickSeconds = 5.0f;
    [SerializeField] private float passiveEnergyTickAmount = 1.0f;
    private float passiveEnergyCooldown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = totalEnergy;

    }

    // Update is called once per frame
    void Update(){
        if(Input.GetButton("Horizontal"))       // (a, d) or (<-, ->)
            MovePlayer();
        if (Input.GetButton("Tilt"))            // (mouse 0, mouse 2) or (q, e)
            TiltPlayer();
        if (Input.GetButton("SpendEnergy")) {   // spacebar or w
            ActivelySpendEnergy();
        }
        else {
            PassivelySpendEnergy();
        }
    }

    private void MovePlayer() {
        if (Input.GetAxis("Horizontal") > 0){
            gameObject.transform.position += new Vector3(movementSpeed * Time.deltaTime, 0.0f, 0.0f);
            for (int i = 0; i < listOfCaughtShapes.Count; i++){
                listOfCaughtShapes[i].MoveWithPlatform(movementSpeed * Time.deltaTime);
            }
        }

        else {
            gameObject.transform.position += new Vector3(-1.0f * movementSpeed * Time.deltaTime, 0.0f, 0.0f);
            for (int i = 0; i < listOfCaughtShapes.Count; i++){
                listOfCaughtShapes[i].MoveWithPlatform(-1.0f * movementSpeed * Time.deltaTime);
            }
        }
           
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

    private void ActivelySpendEnergy() {
        passiveEnergyCooldown = 0.0f;
        //switch()
    }

    private void PassivelySpendEnergy() {
        passiveEnergyCooldown += Time.deltaTime;
        if (passiveEnergyCooldown >= passiveEnergyTickSeconds) {
            currentEnergy -= passiveEnergyTickAmount;
            passiveEnergyCooldown = 0.0f;    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Catchable>() != null) {
            listOfCaughtShapes.Add(collision.gameObject.GetComponent<Catchable>());
        }
    }
}
