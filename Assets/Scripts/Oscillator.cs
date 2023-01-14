using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{   
    [SerializeField]
    Vector3 movementVector = new Vector3(0, 30, 0);
    
    Vector3 startingPos;

    [Range(0, 1)]
    [SerializeField]
    float movementFactor;

    [SerializeField]
    float period = 4f;
    
    // Start is called before the first frame update
    void Start(){
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update(){
        float cycles = Time.time / period; // grows continually from 0
        const float tau = Mathf.PI * 2f; // = 6.28 
        float rawSinWave = Mathf.Sin(cycles * tau); //from -1 to 1

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset; 
    }
}
