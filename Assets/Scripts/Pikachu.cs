using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikachu : MonoBehaviour {
    public GameObject smoke;
    public GameObject model;

    void Start() {
        model.SetActive(false);
        smoke.SetActive(true);
        StartCoroutine(waitAndShowModel());
    }

    IEnumerator waitAndShowModel() {
        yield return new WaitForSeconds(.2f);
        model.SetActive(true);
    }
}
