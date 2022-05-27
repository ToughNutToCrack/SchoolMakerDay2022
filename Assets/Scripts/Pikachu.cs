using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikachu : MonoBehaviour {
    public GameObject smoke;
    public GameObject model;
    public float speed = 1;

    void Start() {
        model.SetActive(false);
        smoke.SetActive(true);
        StartCoroutine(waitAndShowModel());
    }

    IEnumerator waitAndShowModel() {
        yield return new WaitForSeconds(.2f);
        model.SetActive(true);
    }

    public void capture(Vector3 pokePos)
    {
        StartCoroutine(disappear(pokePos));
    }

    IEnumerator disappear(Vector3 pokePos)
    {
        var direction = pokePos - model.transform.position;
        while (model.transform.localScale.magnitude > 0.05f)
        {
            model.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            model.transform.position += direction * speed * Time.deltaTime; 
            yield return null;
        }
        model.transform.localScale = Vector3.zero;
        model.transform.position = pokePos;
    }
}
