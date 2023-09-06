using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catchable : MonoBehaviour
{
    [SerializeField] private float xSpawn = 0.0f;
    [SerializeField] private float ySpawn = 0.0f;
    [SerializeField] private float energyRequired = 0.0f;
    [SerializeField] private float energyReturn = 0.0f;
    private float currentEnergy = 0.0f;

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

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.GetComponent<Player>() != null) {
            Debug.Log("Exit has been detected");
            Despawn();
            Respawn();
            gameObject.SetActive(false);
        }
    }

    private void Despawn(bool destroy = false) {
        if (destroy) {
            Object.Destroy(this);
            return;
        }
        Player.listOfCaughtShapes.Remove(this);
        gameObject.SetActive(false);
    }

    private void Respawn() {
        gameObject.transform.position = new Vector3(xSpawn, ySpawn, 0.0f);
        currentEnergy = 0.0f;
    }

    public float ConsumeEnergy(float energy) {
        currentEnergy += energy;
        if (currentEnergy >= energyRequired) {
            return energyReturn;
        }
        return 0.0f;
    }
}
