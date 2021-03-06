using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARRaycastManager))]
public class GameLogiController : MonoBehaviour {

    public Transform cameraTransform;
    [Space]
    public GameObject pokemon;
    public GameObject pokeball;
    [Space]
    public float strength = 1;

    public static GameLogiController Instance { get; private set; }

    ARRaycastManager raycastManager;
    ARPlaneManager planeManager;
    GameObject spawnedPokemon;


    void Awake() {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    void Update() {

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {

                if (spawnedPokemon == null) {
                    var touchPos = touch.position;

                    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

                    if (raycastManager.Raycast(touchPos, hitResults, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon)) {
                        var hitPose = hitResults[0].pose;
                        spawnedPokemon = Instantiate(pokemon, hitPose.position, hitPose.rotation);
                        AudioManager.Instance.PlayPikaSound();
                        handlePlaneDetection(false);
                    }
                } else {
                    var spawnPos = cameraTransform.position + cameraTransform.forward * 0.1f;
                    var pokeballObject = Instantiate(pokeball, spawnPos, cameraTransform.rotation);
                    var rb = pokeballObject.GetComponent<Rigidbody>();
                    rb.AddForce((cameraTransform.forward + Vector3.up) * strength, ForceMode.Impulse);
                }
            }
        }
    }

    public void resetGameLogic()
    {
        spawnedPokemon = null;
        handlePlaneDetection(true);
    }

    void handlePlaneDetection(bool activate) {
        planeManager.enabled = activate;
        setAllPlanesActive(activate);
    }

    void setAllPlanesActive(bool value) {
        foreach (var plane in planeManager.trackables)
            plane.gameObject.SetActive(value);
    }
}
