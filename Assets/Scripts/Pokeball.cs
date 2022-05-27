using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PokeState {
    Idle,
    Capturing,
    Full
}

public class Pokeball : MonoBehaviour {
    const string ANIMTRIGGER = "Action";
    const string PIKATAG = "Pika";

    public float captureTime = 2;
    public GameObject hitVfx;
    public GameObject capturedVfx;
    Animator anim;

    PokeState state;
    Rigidbody rb;
    float elapsedTime;
    Pikachu pika;

    private void Start() {
        state = PokeState.Idle;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        hitVfx.SetActive(false);
        capturedVfx.SetActive(false);
    }

    private void Update() {
        if (state == PokeState.Capturing) {
            if (elapsedTime < captureTime) {
                elapsedTime += Time.deltaTime;
            } else {
                state = PokeState.Full;
                anim.SetTrigger(ANIMTRIGGER);
                rb.isKinematic = false;
                capturedVfx.SetActive(true);
                StartCoroutine(DestroyAndNotify());
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag(PIKATAG) && state == PokeState.Idle) {
            pika = collision.collider.GetComponentInParent<Pikachu>();
            if (pika.isFree)
            {
                state = PokeState.Capturing;
                rb.isKinematic = true;
                anim.SetTrigger(ANIMTRIGGER);
                AudioManager.Instance.PlayPokeSound();
                hitVfx.SetActive(true);
                pika.capture(transform.position);
            }
            
        }
    }

    IEnumerator DestroyAndNotify() {
        float elapsedTime = 0;
        while (elapsedTime < 2) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GameLogiController.Instance.resetGameLogic();
        Destroy(gameObject);
        Destroy(pika.gameObject);
    }
}
