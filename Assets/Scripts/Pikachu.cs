using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikachu : MonoBehaviour {
    const string WEIGHT = "_Weight";
    public GameObject smoke;
    public GameObject model;
    public Renderer pikaRenderer;
    [Space]
    public float speed = 0.5f;
    public float vfxSpeed = 1;
    public float moveSpeed = 1;
    public bool isFree;
    [Space]
    Material[] materials;

    void Start() {
        isFree = true;
        model.SetActive(false);
        smoke.SetActive(true);
        materials = pikaRenderer.materials;
        resetMaterial();
        StartCoroutine(waitAndShowModel());
    }

    void resetMaterial() {
        foreach (var m in materials) {
            m.SetFloat(WEIGHT, 0);
        }
    }

    IEnumerator waitAndShowModel() {
        yield return new WaitForSeconds(.2f);
        model.SetActive(true);
    }

    public void capture(Vector3 pokePos) {
        isFree = false;
        StartCoroutine(disappear(pokePos));
    }

    IEnumerator disappear(Vector3 pokePos) {
        float vfxColorTransition = 0;
        while (model.transform.localScale.magnitude > 0.05f) {
            model.transform.localScale -= Vector3.one * speed * Time.deltaTime;
            model.transform.position = Vector3.Lerp(model.transform.position, pokePos, moveSpeed * Time.deltaTime);
            vfxColorTransition += vfxSpeed * Time.deltaTime;
            foreach (var m in materials) {
                m.SetFloat(WEIGHT, vfxColorTransition);
            }
            yield return null;
        }
        model.transform.localScale = Vector3.zero;
        model.transform.position = pokePos;
        foreach (var m in materials) {
            m.SetFloat(WEIGHT, 1);
        }
    }

    void OnDestroy() {
        resetMaterial();
    }
}
