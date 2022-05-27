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
    public float captureTime = 2;
    Animator anim;
    const string ANIMTRIGGER = "Action";
    const string PIKATAG = "Pika";
    PokeState state;
    Rigidbody rb;
    float elapsedTime;
    GameObject pika;

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
                StartCoroutine(DestroyAndNotify());
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
            collision.collider.GetComponentInParent<Pikachu>().capture(transform.position);
        }
    }

    IEnumerator DestroyAndNotify()
    {
        float elapsedTime = 0;
        while (elapsedTime < 2)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GameLogiController.Instance.resetGameLogic();
        Destroy(gameObject);
        Destroy(pika.gameObject);
    }
}
