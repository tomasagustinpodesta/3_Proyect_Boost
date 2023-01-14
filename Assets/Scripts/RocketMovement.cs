using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketMovement : MonoBehaviour
{       
    //Attributes
    Rigidbody rigidbody;
    AudioSource audioSource;

    [SerializeField] float rotationIntensity = 150f;
    [SerializeField] float boostIntensity = 1000f;

    [SerializeField] State state = State.alive;

    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip rocketSound;

    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem rocketParticle;

    enum State {alive, dying, leveling, test}
    

    // Start is called before the first frame update
    void Start(){
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        if(state == State.alive || state == State.test){
            RocketRotation();
            RocketBoost();
        }
        
        if(Debug.isDebugBuild){
            Dev();
        }
        
    }

    private void RocketRotation(){
        rigidbody.freezeRotation = true;
        
        if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward * rotationIntensity * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward * rotationIntensity * Time.deltaTime);
        }

        rigidbody.freezeRotation = false;
    }

    void Dev(){
        if(Input.GetKey(KeyCode.C)){
            if(state == State.alive){
                state = State.test;
            } else 
                state = State.alive;
        }

        if(Input.GetKey(KeyCode.L)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void RocketBoost(){
        if(Input.GetKey(KeyCode.Space)){
            rigidbody.AddRelativeForce(Vector3.up * boostIntensity * Time.deltaTime);

            if (!audioSource.isPlaying){
                rocketParticle.Play();
                audioSource.PlayOneShot(rocketSound);
            }
        }
        else{
            audioSource.Stop();
            rocketParticle.Stop();
        }
    }

    void OnCollisionEnter(Collision collision){
        if(state == State.alive){
            switch (collision.gameObject.tag){
                case "Friendly":
                    //nothing
                    break;
                
                case "Finish":
                    audioSource.Stop();
                    state = State.leveling;
                    AudioAndEffect();
                    Invoke("WinLevel", 1f);
                    break;

                default:
                    state = State.dying;
                    audioSource.Stop();
                    deathParticle.Play();
                    audioSource.PlayOneShot(deathSound); //It does work here
                    Invoke("LooseLevel", 1f);
                    break;
            }    
            
        }
    }

    void AudioAndEffect(){
        audioSource.Stop();
        audioSource.PlayOneShot(successSound); //It does work here
        successParticle.Play();
    }
    void WinLevel(){
        //audioSource.PlayOneShot(successSound); //It doesnt work here, ignores it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void LooseLevel(){   
        //audioSource.PlayOneShot(deathSound); //It doesnt work here, ignores it
        deathParticle.Play();
        SceneManager.LoadScene(0);
    } 
}
