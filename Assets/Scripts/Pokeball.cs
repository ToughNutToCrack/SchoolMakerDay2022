using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PokeState
{
    Idle,
    Capturing,
    Full
}

public class Pokeball : MonoBehaviour
{
    public float captureTime = 3;
    public AudioClip pokeSound;
    Animator anim;
    const string ANIMTRIGGER = "Action";
    const string PIKATAG = "Pika";
    PokeState state;
    Rigidbody rb;
    float elapsedTime;

    private void Start()
    {
        state = PokeState.Idle;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (state == PokeState.Capturing)
        {
            if (elapsedTime < captureTime)
            {
                elapsedTime += Time.deltaTime;
            }
            else
            {
                state = PokeState.Full;
                anim.SetTrigger(ANIMTRIGGER);
                rb.isKinematic = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(PIKATAG) && state == PokeState.Idle)
        {
            state = PokeState.Capturing;
            rb.isKinematic = true;
            anim.SetTrigger(ANIMTRIGGER);
            AudioManager.Instance.PlayPokeSound();
        }
    }
}
