using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Rotate : MonoBehaviour
{   
    [SerializeField]
    Vector3 rotationVector = new Vector3(1, 0, 0);
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        transform.Rotate(rotationVector / 1000); 
    }
}
