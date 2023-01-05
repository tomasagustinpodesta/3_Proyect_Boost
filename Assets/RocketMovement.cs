using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{       
    //Attributes
    Rigidbody rigidbody;
    AudioSource rocketSound;
    // Start is called before the first frame update
    void Start(){
        rigidbody = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        ProcessInput();
    }

    private void ProcessInput(){
        if (Input.GetKey(KeyCode.Space)){
            rigidbody.AddRelativeForce(Vector3.up);

            if (!rocketSound.isPlaying){
                rocketSound.Play();
            }
        }
        else{
            rocketSound.Stop();
        }


        if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward);
        }

        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward);
        }
    }
}
