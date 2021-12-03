using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitPlayAudio : MonoBehaviour
{

    AudioSource audioSource;

    [SerializeField] AudioClip onPinHit;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    private void PlayAudio(AudioClip clip,float volume)
    {
        
         audioSource.clip = clip;
               
                audioSource.volume = volume;
                audioSource.Play();
    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.collider.CompareTag("Ball"))
        {
             Rigidbody rigidbody = other.collider.GetComponent<Rigidbody>();
            float force = rigidbody.velocity.magnitude;
            PlayAudio(onPinHit, force);

        }
    }
}